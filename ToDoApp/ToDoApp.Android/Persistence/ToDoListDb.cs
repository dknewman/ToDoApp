using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQLite;
using ToDoApp.Droid.Persistence;
using ToDoApp.Persistence;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToDoListDb))]
namespace ToDoApp.Droid.Persistence
{
    class ToDoListDb : IToDoListDb    
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "ToDoListSqlLite.db3");
            return new SQLiteAsyncConnection(path);
        }

    }
}