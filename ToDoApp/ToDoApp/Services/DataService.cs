using System;
using System.Diagnostics;
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
            try
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
            catch (Exception ex)
            {
                //Handle exception - Typically I use a service like rollbar 
                Debug.WriteLine(ex);
            }
        }

        public static async Task SaveNewListItem()
        {
            try
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
            catch (Exception ex)
            {
                //Handle exception - Typically I use a service like rollbar 
                Debug.WriteLine(ex);
            }
        }


        public static async Task DeleteToDoList(ToDoListModel toDoDelete)
        {
            try
            {
                await using var toDoContext = new ToDoContext();
                toDoContext.Remove(toDoDelete);
                await toDoContext.SaveChangesAsync();
                PostToDoList(toDoContext);
            }
            catch (Exception ex)
            {
                //Handle exception - Typically I use a service like rollbar 
                Debug.WriteLine(ex);
            }
        }

        public static async Task DeleteToDoItem(ToDoItemModel toDoDelete)
        {
            try
            {
                await using var toDoContext = new ToDoContext();
                toDoContext.Remove(toDoDelete);
                await toDoContext.SaveChangesAsync();
                PostToDoItem(toDoContext);
            }
            catch (Exception ex)
            {
                //Handle exception - Typically I use a service like rollbar 
                Debug.WriteLine(ex);
            }
        }

        public static void PostToDoList(ToDoContext toDoContext)
        {
            try
            {
                var httpService = new HttpService();
                httpService.PostToServer(toDoContext.ToDoListModel, "PostToDoList");
            }
            catch (Exception ex)
            {
                //Handle exception - Typically I use a service like rollbar 
                Debug.WriteLine(ex);
            }
        }

        public static void PostToDoItem(ToDoContext toDoContext)
        {
            try
            {
                var httpService = new HttpService();
                httpService.PostToServer(toDoContext.ToDoItemModel, "PostToDoItem");
            }
            catch (Exception ex)
            {
                //Handle exception - Typically I use a service like rollbar 
                Debug.WriteLine(ex);
            }
        }
    }
}
