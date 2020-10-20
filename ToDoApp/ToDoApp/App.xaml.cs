using System;
using System.IO;
using ToDoApp.Services;
using ToDoApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            var cs = new ConnectivityService();
            cs.CheckConnectivity();
        }

        
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
