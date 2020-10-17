using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQLite;
using ToDoApp.iOS.Persistence;
using ToDoApp.Persistence;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToDoListDb))]

namespace ToDoApp.iOS.Persistence
{
   public class ToDoListDb : IToDoListDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "ToDoListSqlLite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}