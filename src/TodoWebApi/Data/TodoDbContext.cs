using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Core.Models;

namespace TodoWebApi.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<Todo> Todo { get; set;  }


    }
}
