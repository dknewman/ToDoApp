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
            // Register for connectivity changes, be sure to unsubscribe when finished
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;
        }
    }
}
