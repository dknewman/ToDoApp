using System.Diagnostics;
using Xamarin.Essentials;

namespace ToDoApp.Services
{
    public class ConnectivityService
    {
                
        public ConnectivityService()
        {
           
        }

        public void CheckConnectivity()
        {
            // Subscribe for connectivity changes
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
        }
    }
}
