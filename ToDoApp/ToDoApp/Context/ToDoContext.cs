using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;
using Xamarin.Essentials;

namespace ToDoApp.Context
{
    public class ToDoContext: DbContext
    {
        public DbSet<ToDoListModel> ToDoListModel { get; set; }
        public DbSet<ToDoItemModel> ToDoItemModel { get; set; }

        public ToDoContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ToDoList.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
