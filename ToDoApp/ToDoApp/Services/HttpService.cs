using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class HttpService
    {
        public void PostToServer(object listOrItemObject, string postType)
        {
            string jsonData = JsonConvert.SerializeObject(listOrItemObject);
            var convertedJsonStr = JsonConvert.ToString(jsonData);
            var client = new RestClient($"https://davidnewman.pro/todo/{postType}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", convertedJsonStr, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

        }

        public  void GetToDoListsOrItems(string getToDoListOrItem)
        {
            ObservableCollection<ToDoListModel> rentalsObject = new ObservableCollection<ToDoListModel>();
            var client = new RestClient("https://davidnewman.pro/todo/{getToDoListOrItem}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            rentalsObject = JsonConvert.DeserializeObject<ObservableCollection<ToDoListModel>>(response.Content);
            Debug.WriteLine("");

        }
    }
}
