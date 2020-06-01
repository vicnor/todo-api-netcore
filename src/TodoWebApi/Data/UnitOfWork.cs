using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApi.Core;
using TodoWebApi.Core.Repositories;
using TodoWebApi.Data.Repositories;

namespace TodoWebApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoDbContext context;
        private TodoRepository todoRepository;

        public UnitOfWork(TodoDbContext context)
        {
            this.context = context;
        }

        public ITodoRepository Todo => todoRepository = todoRepository ?? new TodoRepository(context);

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
