using ToDoApp.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ToDoApp.ViewModels;
using ToDoApp.Views;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
   
    public class ToDoListDetailViewModel : BaseViewModel
    {
        public Command AddToDoListItemCommand { get; }

        public ToDoListDetailViewModel()
        {
            AddToDoListItemCommand = new Command(OnAddListAsync);
        }

        private async void OnAddListAsync(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CreateNewItemPage());
        }

        private string _toDoListItemText;

        public string ToDoItem
        {
            get => _toDoListItemText;
            set
            {
                SetValue(ref _toDoListItemText, value);
                OnPropertyChanged(nameof(ToDoItem));
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;

        }
    }
}
