using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            var httpService = new HttpService();
            httpService.PostToServer(toDoContext.ToDoListModel);
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

        }

        public static async Task DeleteToDoList(ToDoListModel toDoDelete)
        {
            await using var toDoContext = new ToDoContext();
            toDoContext.Remove(toDoDelete);
            await toDoContext.SaveChangesAsync();
        }

        public static async Task DeleteToDoItem(ToDoItemModel toDoDelete)
        {
            await using var toDoContext = new ToDoContext();
            toDoContext.Remove(toDoDelete);
            await toDoContext.SaveChangesAsync();
        }
    }

}
