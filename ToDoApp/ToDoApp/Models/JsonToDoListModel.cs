using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ToDoApp.Models
{
    [JsonArray]
    public class JsonToDoListModel
    {
        [JsonProperty("ToDoListModelId")]
        public int ToDoListModelId { get; set; }
        [JsonProperty("ListName")]
        public string ListName { get; set; }
        [JsonProperty("ToDoItems")]
        public List<ToDoItemModel> ToDoItems { get; set; } = new List<ToDoItemModel>();
        [JsonProperty("LastUpdate")]
        public DateTime LastUpdate { get; set; }
    }
}
