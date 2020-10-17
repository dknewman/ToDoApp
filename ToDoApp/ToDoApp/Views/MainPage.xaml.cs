using System;
using System.Diagnostics;
using ToDoApp.ViewModels;
using Xamarin.Forms;

namespace ToDoApp.Views
{
    public partial class MainPage : ContentPage 
    {
        private MainViewModel _mainViewModel;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _mainViewModel = new MainViewModel();
        }

        private void ToolBarButton_OnClicked(object sender, EventArgs e)
        {
           Debug.WriteLine("Tool Bar Button Pressed.");
        }

        private void MyList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           //do something///
        }
    }
}
