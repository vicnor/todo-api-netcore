using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Core.Models;

namespace TodoWebApi.Data
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Todo> Todo { get; set;  }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=todo.db");

    }
}
