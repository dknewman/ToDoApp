using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using ToDoApp.Models;
using ToDoApp.Views;
using Xamarin.Forms;


namespace ToDoApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Command AddListCommand { get; }
 
        public Command<ToDoListModel> ListTapped { get; }

        public MainViewModel()
        {
            AddListCommand = new Command(OnAddList);
        }

        private async void OnAddList(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CreateNewListPage());
        }

        private string _listName;
        public string ListName
        {
            get => _listName;
            set
            {
                SetValue(ref _listName, value);
                OnPropertyChanged(nameof(ListName));
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
