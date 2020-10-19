using ToDoApp.ViewModels;
using System.ComponentModel;
using ToDoApp.ViewModels;
using Xamarin.Forms;

namespace ToDoApp.Views
{
    public partial class ListDetailPage : ContentPage
    {
        public ListDetailPage()
        {
            InitializeComponent();
            BindingContext = new ListDetailViewModel();
        }
    }
}