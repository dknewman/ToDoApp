using System.Diagnostics;
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
            CheckConnectivity();
        }
        public static void CheckConnectivity()
        {
            var networkAccess = Connectivity.NetworkAccess;
            if (networkAccess == NetworkAccess.Internet)
            {
                HasInternet = true;
            }
            else
            {
                HasInternet = false;
            }
        }
    }
}
