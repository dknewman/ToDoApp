using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using ToDoApp.Context;
using ToDoApp.Models;
using ToDoApp.Views;

namespace ToDoApp.Services
{
    public class HttpService
    {

        public void PostToServer(object listOrItemObject, string postType)
        {
            if (ConnectivityService.HasInternet)
            {
                PostData(listOrItemObject, postType);
            }
        }

        private static void PostData(object listOrItemObject, string postType)
        {
            string jsonData = JsonConvert.SerializeObject(listOrItemObject);
            var convertedJsonStr = JsonConvert.ToString(jsonData);
            var client = new RestClient($"https://davidnewman.pro/todo/{postType}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", convertedJsonStr, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

        public static async Task<ObservableCollection<ToDoListModel>> GetToDoListTask()
        {
            ObservableCollection<ToDoListModel> toDoListObject = new ObservableCollection<ToDoListModel>();
            HttpClient hc = new HttpClient();
            var listUrl = "https://davidnewman.pro/todo/GetToDoList";
            var listContents = await hc.GetStringAsync(listUrl);
            toDoListObject = JsonConvert.DeserializeObject<ObservableCollection<ToDoListModel>>(listContents);

            return toDoListObject;
        }

        public static async Task<ObservableCollection<ToDoItemModel>> GetTodoItemTask(int toDoListModelId)
        {
            ObservableCollection<ToDoItemModel> toDoItemObject = new ObservableCollection<ToDoItemModel>();
            HttpClient hc = new HttpClient();
            var Url = "https://davidnewman.pro/todo/GetToDoItem/" + toDoListModelId;
            var contents = await hc.GetStringAsync(Url);
            toDoItemObject = JsonConvert.DeserializeObject<ObservableCollection<ToDoItemModel>>(contents);

            return toDoItemObject;
        }
        public static async Task<ToDoListModel> GetToDoListModel()
        {
            var toDoListObject = new ToDoListModel();
            HttpClient hc = new HttpClient();
            var listUrl = "https://davidnewman.pro/todo/GetToDoList";
            var listContents = await hc.GetStringAsync(listUrl);
            toDoListObject = JsonConvert.DeserializeObject<ToDoListModel>(listContents);

            return toDoListObject;
        }
        public static async Task<DateTime> GetLastListDataEntryTask()
        {
            var appListData = new StoreAppDataModel();
            HttpClient hc = new HttpClient();
            var Url = "https://davidnewman.pro/todo/GetToDoListData";
            var contents = await hc.GetStringAsync(Url);
            appListData = JsonConvert.DeserializeObject<StoreAppDataModel>(contents);
            var getLastDateTime = appListData.LastUpdate;

            return getLastDateTime;
        }

        public static async Task<DateTime> GetLastItemDataEntryTask()
        {
            var appItemData = new StoreItemDataModel();
            HttpClient hc = new HttpClient();
            var Url = "https://davidnewman.pro/todo/GetToDoItemData";
            var contents = await hc.GetStringAsync(Url);
            appItemData = JsonConvert.DeserializeObject<StoreItemDataModel>(contents);
            var getLastDateTime = appItemData.LastUpdate;

            return getLastDateTime;
        }

    }
}
