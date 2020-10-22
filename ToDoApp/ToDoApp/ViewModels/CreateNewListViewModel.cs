using System.Diagnostics;
using System.Threading.Tasks;
using ToDoApp.Context;
using ToDoApp.Models;
using ToDoApp.Persistence;
using Xamarin.Forms;
using static System.String;

namespace ToDoApp.ViewModels
{
    class CreateNewListViewModel : BaseViewModel
    {
        public static string ListName;

        public CreateNewListViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !IsNullOrWhiteSpace(ListName);
        }

        public string NewListName
        {
            get => ListName;
            set => SetValue(ref ListName, value);
        }
        
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            ListName = Empty;
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }

        private async void OnSave()
        {
            await DataAccess.SaveNewList();
            ListName = Empty;
            // This will pop the current page off the navigation stack
            await Application.Current.MainPage.Navigation.PopModalAsync(true);

        }

    }
}
