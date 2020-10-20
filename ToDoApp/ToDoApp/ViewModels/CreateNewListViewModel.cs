using System;
using System.Diagnostics;
using ToDoApp.Context;
using ToDoApp.Models;
using Xamarin.Forms;

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
            var addToDoList = new ToDoListModel
            {
                ListName = NewListName,
                LastUpdate = DateTime.Now
            };

            using (var toDoContext = new ToDoContext())
            {
                toDoContext.Add(addToDoList);
                await toDoContext.SaveChangesAsync();
            }

            Debug.WriteLine("Save Command Hit");
            // This will pop the current page off the navigation stack
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }
    }
}
