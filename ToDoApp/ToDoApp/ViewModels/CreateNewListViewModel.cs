using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ToDoApp.Models;
using Xamarin.Forms;
using ToDoApp.Helpers;

namespace ToDoApp.ViewModels
{
    class CreateNewListViewModel : BaseViewModel
    {
        private string _newListName;

        public CreateNewListViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_newListName);

        }

        public string NewListName
        {
            get => _newListName;
            set => SetValue(ref _newListName, value);
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }

        private async void OnSave()
        {
            await App.Database.SaveToDoListAsync(new ToDoListModel
            {
            
                ListName = NewListName

            });
            // await DataStore.AddItemAsync(newItem);
            
            Debug.WriteLine("Save Command Hit");
            // This will pop the current page off the navigation stack
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }
    }
}
