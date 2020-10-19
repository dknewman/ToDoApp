using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ToDoApp.Models
{
    public class ToDoListModel
    {
        [PrimaryKey, AutoIncrement]
        public int ListModelId { get; set; }
        public string ListName { get; set; }
        [TextBlob("ToDoItems")]
        public List<ToDoItemModel> ToDoItems { get; set; }
    }
}
