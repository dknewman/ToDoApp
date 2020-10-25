using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToDoApp.Context;
using ToDoApp.Models;
using ToDoApp.ViewModels;
using ToDoApp.Views;

namespace ToDoApp.Services
{
    public class DataService
    {
        
        public static async Task SaveNewList()
        {
            var addToDoList = new ToDoListModel
            {
                ListName = CreateNewListViewModel.ListName,
                LastUpdate = DateTime.Now
            };

            await using var toDoContext = new ToDoContext();
            toDoContext.Add(addToDoList);
            await toDoContext.SaveChangesAsync();
            PostToDoList(toDoContext);
        }

        public static async Task SaveNewListItem()
        {
            var newToDoItem = new ToDoItemModel()
            {
                ToDoItem = CreateNewItemViewModel.NewItem,                
                LastUpdate = DateTime.Now
            };

            await using var toDoContext = new ToDoContext();
            var toDoList = await toDoContext
                .ToDoListModel
                .FirstOrDefaultAsync(x => x.ToDoListModelId == MainPage.ToDoLists.ToDoListModelId);

            toDoList.ToDoItems.Add(newToDoItem);
            await toDoContext.SaveChangesAsync();
            PostToDoItem(toDoContext);
        }


        public static async Task DeleteToDoList(ToDoListModel toDoDelete)
        {
            await using var toDoContext = new ToDoContext();
            toDoContext.Remove(toDoDelete);
            await toDoContext.SaveChangesAsync();
            PostToDoList(toDoContext);
            PostToDoItem(toDoContext);
        }

        public static async Task DeleteToDoItem(ToDoItemModel toDoDelete)
        {
            await using var toDoContext = new ToDoContext();
            toDoContext.Remove(toDoDelete);
            await toDoContext.SaveChangesAsync();
            PostToDoItem(toDoContext);
        }

        public static void PostToDoList(ToDoContext toDoContext)
        {
            var httpService = new HttpService();
            httpService.PostToServer(toDoContext.ToDoListModel, "PostToDoList");
        }

        private static void PostToDoItem(ToDoContext toDoContext)
        {
            var httpService = new HttpService();
            httpService.PostToServer(toDoContext.ToDoItemModel, "PostToDoItem");
        }

    }

}
