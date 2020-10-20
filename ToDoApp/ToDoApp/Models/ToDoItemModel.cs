using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Models
{
    public class ToDoItemModel
    {
        public int ToDoItemModelId { get; set; }
        public string ToDoItem { get; set; }
        public int ToDoListModelId { get; set; }
        public ToDoListModel ToDoListModel { get; set; }
    }
}
