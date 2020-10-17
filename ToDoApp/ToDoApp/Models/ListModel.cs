using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Models
{
    public class ListModel
    {
        public int ListModelId { get; set; }
        public string ListName { get; set; }
        public List<ToDoItemModel> ToDoItems { get; set; }
    }
}
