using System;
using System.IO;
using ToDoApp.Helpers;
using ToDoApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp
{
    public partial class App : Application
    {
        static SQLiteHelper _database;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public static SQLiteHelper Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new SQLiteHelper(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ToDoList.db3"));
                }

                return _database;
            }
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
