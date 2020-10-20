using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Context;
using ToDoApp.Models;
using ToDoApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel _mainViewModel;
        public static ToDoListModel ToDoLists { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _mainViewModel = new MainViewModel();
        }

        private void ToolBarButton_OnClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Tool Bar Button Pressed.");
        }

        private async void MyList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ToDoLists = (ToDoListModel)e.SelectedItem;
            await Navigation.PushAsync(new ToDoListDetailPage());
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _mainViewModel.OnAppearing();
            await RefreshListView();
        }

        private async Task RefreshListView()
        {
            //myList.ItemsSource = await App.Database.GetListAsync();

            using (var toDoContext = new ToDoContext())
            {
                myList.ItemsSource = await toDoContext.ToDoListModel.ToListAsync();
            }

        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            ToDoListModel ToDoDelete = new ToDoListModel();
            ToDoDelete = (ToDoListModel)mi.CommandParameter;
           var deleteConfirmation = await DisplayAlert("Delete ToDo List", "Are you sure you want to delete " + ToDoDelete.ListName + "?", "OK", "Cancel");

           if (deleteConfirmation)
           {
               using (var toDoContext = new ToDoContext())
               {
                   toDoContext.Remove(ToDoDelete);
                   await toDoContext.SaveChangesAsync();
               }

                await RefreshListView();
            }
        }
    }
}
