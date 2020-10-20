using System;
using ToDoApp.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Context;
using ToDoApp.Models;
using ToDoApp.ViewModels;
using Xamarin.Forms;

namespace ToDoApp.Views
{
    public partial class ToDoListDetailPage : ContentPage
    {
        private ToDoListDetailViewModel _toDoListDetailViewModel;
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

        private void OnDelete(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private async Task RefreshListView()
        {
            //myList.ItemsSource = await App.Database.GetListAsync();
            try
            {
                await using (var toDoContext = new ToDoContext())
                {
                    var itemList = toDoContext.ToDoItemModel
                        .Where(x => x.ToDoListModelId == MainPage.ToDoLists.ToDoListModelId)
                        .ToList();

                    myList.ItemsSource = itemList;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}