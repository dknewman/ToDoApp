using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ToDoApp.Models;


namespace ToDoApp.ViewModels
{
   public class MainViewModel : BaseViewModel
   {
      
        public int Id { get; set; }
        public MainViewModel()
        {
        }

        public MainViewModel(ToDoListModel toDoList)
        {
            Id = toDoList.ListModelId;
            ListName = toDoList.ListName;
        }

        private string _listName;
        public string ListName
        {
            get { return _listName; }
            set
            {
                SetValue(ref _listName, value);
                OnPropertyChanged(nameof(ListName));
            }
        }
    }
}
