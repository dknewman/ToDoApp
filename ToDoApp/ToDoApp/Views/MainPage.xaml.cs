using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using ToDoApp.Context;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace ToDoApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel MainViewModel;
        public static ToDoListModel ToDoLists { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = MainViewModel = new MainViewModel();
        }

        private async void MyList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            ToDoLists = (ToDoListModel)e.SelectedItem;
            await Navigation.PushAsync(new ToDoListDetailPage());

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MainViewModel.OnAppearing();
            await RefreshListView();
        }

        private async Task RefreshListView()
        {
            if (ConnectivityService.HasInternet)
            {
                myList.ItemsSource = await HttpService.GetToDoListTask();
                if (!myList.ItemsSource.OfType<ToDoListModel>().Any())
                {
                    await DisplayAlert("Welcome to the ToDo App!", "Add a new list and get started", "OK");
                }
            }
            else
            {
                await using var toDoContext = new ToDoContext();
                myList.ItemsSource = await toDoContext.ToDoListModel.ToListAsync();
                Debug.Write("");
            }
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var toDoDelete = (ToDoListModel)mi.CommandParameter;
            var deleteConfirmation = await DisplayAlert("Delete ToDo List", "Are you sure you want to delete " + toDoDelete.ListName + "?", "OK", "Cancel");
            if (deleteConfirmation)
            {
                await DataService.DeleteToDoList(toDoDelete);
                await RefreshListView();
            }
        }

    }
}
