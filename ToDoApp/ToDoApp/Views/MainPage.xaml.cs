using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.ghostshaman.com/");

            string jsonData = JsonConvert.SerializeObject(ToDoLists);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/todo/PostToDoList", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();
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

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            //
        }
    }
}
