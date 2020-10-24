using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Context;
using ToDoApp.Models;
using ToDoApp.Services;
using Xamarin.Forms;
using ToDoApp.Views;
using static System.String;

namespace ToDoApp.ViewModels
{
    public class CreateNewItemViewModel : BaseViewModel
    {
        public static string NewItem;

        public CreateNewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !IsNullOrWhiteSpace(NewItem);
        }

        public string NewListItem
        {
            get => NewItem;
            set => SetValue(ref NewItem, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            NewItem = Empty;
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }

        private async void OnSave()
        {
            await DataService.SaveNewListItem();
            // This will pop the current page off the navigation stack
            NewItem = Empty;
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }
    }
}
