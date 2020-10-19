using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ToDoApp.Helpers;
using ToDoApp.Models;
using ToDoApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel _mainViewModel;
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
            //do something///
            await Navigation.PushAsync(new ListDetailPage());

            Debug.Write("Tappa tappa tappa");
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _mainViewModel.OnAppearing();
            await RefreshListView();
        }

        private async Task RefreshListView()
        {
            myList.ItemsSource = await App.Database.GetListAsync();
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            ToDoListModel ToDoDelete = new ToDoListModel();
            ToDoDelete = (ToDoListModel)mi.CommandParameter;
           var deleteConfirmation = await DisplayAlert("Delete ToDo List", "Are you sure you want to delete " + ToDoDelete.ListName + "?", "OK", "Cancel");

           if (deleteConfirmation)
           {
               int getListId = ToDoDelete.ListModelId;
               await App.Database.RemoveToDoListEntry(ToDoDelete);
               await RefreshListView();
            }

           
        }
    }
}
