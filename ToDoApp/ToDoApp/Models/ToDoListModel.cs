﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ToDoApp.Models
{
    public class ToDoListModel
    {
        public int ToDoListModelId { get; set; }
        public string ListName { get; set; }
        public List<ToDoItemModel> ToDoItems { get; set; } = new List<ToDoItemModel>();
        public DateTime LastUpdate { get; set; }
    }
}
