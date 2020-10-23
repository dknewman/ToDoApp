using System;
using ToDoApp.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToDoApp.Context;
using ToDoApp.Models;
using ToDoApp.Persistence;
using ToDoApp.ViewModels;
using Xamarin.Forms;

namespace ToDoApp.Views
{
    public partial class ToDoListDetailPage : ContentPage
    {
        private readonly ToDoListDetailViewModel _toDoListDetailViewModel;
        public ToDoListDetailPage()
        {
            InitializeComponent();
            BindingContext = _toDoListDetailViewModel = new ToDoListDetailViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _toDoListDetailViewModel.OnAppearing();
            await RefreshListView();
        }

        private async Task RefreshListView()
        {
            try
            {
                await using var toDoContext = new ToDoContext();
                var itemList = toDoContext.ToDoItemModel
                    .Where(x => x.ToDoListModelId == MainPage.ToDoLists.ToDoListModelId)
                    .ToList();
                myList.ItemsSource = itemList;
                string jsonData = JsonConvert.SerializeObject(itemList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var toDoDelete = (ToDoItemModel)mi.CommandParameter;
            var deleteConfirmation = await DisplayAlert("Delete ToDo Item", "Are you sure you want to delete " + toDoDelete.ToDoItem + "?", "OK", "Cancel");
            if (deleteConfirmation)
            {
                await DataAccess.DeleteToDoItem(toDoDelete);
                await RefreshListView();
            }
        }
    }
}