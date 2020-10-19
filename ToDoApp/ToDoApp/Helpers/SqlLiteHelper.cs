using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using ToDoApp.Models;

namespace ToDoApp.Helpers
{
    public class SQLiteHelper
    {
        readonly SQLiteAsyncConnection _database;

        public SQLiteHelper(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ToDoListModel>().Wait();
        }

        public Task<List<ToDoListModel>> GetListAsync()
        {
            return _database.Table<ToDoListModel>().ToListAsync();
        }

        public Task<int> SaveToDoListAsync(ToDoListModel newTodoList)
        {
            return _database.InsertAsync(newTodoList);
        }

        public Task<int> RemoveToDoListEntry(ToDoListModel toDoListId)
        {
            return _database.DeleteAsync(toDoListId);
        }
    }
}
