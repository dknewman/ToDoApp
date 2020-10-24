using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace ToDoApp.Services
{
    public class HttpService
    {
        public void PostToServer(object listOrItemObject)
        {
            string jsonData = JsonConvert.SerializeObject(listOrItemObject);
            var convertedJsonStr = JsonConvert.ToString(jsonData);
            var client = new RestClient("https://davidnewman.pro/todo/PostToDoList");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", convertedJsonStr, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }
    }
}
