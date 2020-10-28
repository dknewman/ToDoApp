using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
                    await DataService.SyncSqliteToServer();
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
