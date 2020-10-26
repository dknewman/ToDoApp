using System;
using System.Diagnostics;
using System.Linq;
using ToDoApp.Context;
using Xamarin.Essentials;

namespace ToDoApp.Services
{
    public class ConnectivityService
    {
        public static bool HasInternet { get; set; }

        public ConnectivityService()
        {
           
        }

        public void ConnectivityEvent()
        {
            // Subscribe for connectivity changes
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            try
            {
                CheckConnectivity();
            }
            catch (Exception ex)
            {
                //Handle exception - Typically I use a service like rollbar 
                Debug.WriteLine(ex);
            }
        }
        public static async void CheckConnectivity()
        {
            var networkAccess = Connectivity.NetworkAccess;
            if (networkAccess == NetworkAccess.Internet)
            {
                try
                {
                    HasInternet = true;
                    var lastServerListEntryDateTime = await HttpService.GetLastListDataEntryTask();
                    var lastServerItemEntryDateTime = await HttpService.GetLastListDataEntryTask();
                    var toDoContext = new ToDoContext();

                    var getLastListTime = toDoContext.ToDoListModel
                        .OrderByDescending(x => x.LastUpdate)
                        .FirstOrDefault();
                    if (getLastListTime != null)
                    {
                        var lastLocalListEntryDateTime = getLastListTime.LastUpdate;

                        var getLastItemTime = toDoContext.ToDoItemModel
                            .OrderByDescending(x => x.LastUpdate)
                            .FirstOrDefault();
                        if (getLastItemTime != null)
                        {
                            var lastLocalItemEntryDateTime = getLastItemTime.LastUpdate;

                            if (lastLocalListEntryDateTime > lastServerListEntryDateTime)
                            {
                                DataService.PostToDoList(toDoContext);
                            }

                            if (lastLocalItemEntryDateTime > lastServerItemEntryDateTime)
                            {
                                DataService.PostToDoItem(toDoContext);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Handle exception - Typically I use a service like rollbar 
                    Debug.WriteLine(ex);
                }
            }
            else
            {
                HasInternet = false;
            }
        }
    }
}
