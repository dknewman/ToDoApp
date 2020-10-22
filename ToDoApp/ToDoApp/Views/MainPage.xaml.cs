using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Context;
using ToDoApp.Models;
using ToDoApp.Persistence;
using ToDoApp.ViewModels;
using Xamarin.Forms;
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
            await using var toDoContext = new ToDoContext();
            myList.ItemsSource = await toDoContext.ToDoListModel.ToListAsync();
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var toDoDelete = (ToDoListModel)mi.CommandParameter;
            var deleteConfirmation = await DisplayAlert("Delete ToDo List", "Are you sure you want to delete " + toDoDelete.ListName + "?", "OK", "Cancel");
            if (deleteConfirmation)
            {
                await DataAccess.DeleteToDoList(toDoDelete);
                await RefreshListView();
            }
        }
    }
}
