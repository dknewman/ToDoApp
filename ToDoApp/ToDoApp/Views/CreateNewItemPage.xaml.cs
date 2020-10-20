using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNewItemPage : ContentPage
    {
        public ToDoListModel NewList { get; set; }
        public CreateNewItemPage()
        {
            InitializeComponent();
            BindingContext = new CreateNewItemViewModel();
        }
    }
}