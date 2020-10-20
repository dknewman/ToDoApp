using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Context;
using ToDoApp.Models;
using Xamarin.Forms;
using ToDoApp.Views;

namespace ToDoApp.ViewModels
{
    class CreateNewItemViewModel : BaseViewModel
    {
        private string _newListItem;

        public CreateNewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_newListItem);

        }

        public string NewListItem
        {
            get => _newListItem;
            set => SetValue(ref _newListItem, value);
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
 
            var newToDoItem = new ToDoItemModel()
            {
                ToDoItem = NewListItem
            };

            using (var toDoContext = new ToDoContext())
            {
                var toDoList = await toDoContext
                    .ToDoListModel
                    .FirstOrDefaultAsync(x => x.ToDoListModelId == MainPage.ToDoLists.ToDoListModelId);

                toDoList.ToDoItems.Add(newToDoItem);
                await toDoContext.SaveChangesAsync();
            }
          

            Debug.WriteLine("Save Command Hit");
            // This will pop the current page off the navigation stack
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }
    }
}
