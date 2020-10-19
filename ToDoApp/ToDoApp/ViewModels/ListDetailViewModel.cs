using ToDoApp.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ToDoApp.ViewModels;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    [QueryProperty(nameof(ListId), nameof(ListId))]
    public class ListDetailViewModel : BaseViewModel
    {
        private int listId;
        private string listText;
        private string description;
        public string Id { get; set; }

        public string ListText
        {
            get => listText;
            set => SetValue(ref listText, value);
        }

       
        public int ListId
        {
            get => listId;
            set
            {
                listId = value;
                LoadListId(value);
            }
        }

        public async void LoadListId(int itemId)
        {
            try
            {
                //var item = await DataStore.GetItemAsync(itemId);
                //Id = item.Id;
                //Text = item.Text;
                //Description = item.Description;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
