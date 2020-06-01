using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApi.Core;
using TodoWebApi.Core.Models;

namespace TodoWebApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork unitOfWork;

        public TodoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Todo> Add(Todo entity)
        {
            await unitOfWork.Todo.Add(entity);
            await unitOfWork.CommitAsync();
            return await unitOfWork.Todo.GetById(entity.Id);
        }

        public async Task<IEnumerable<Todo>> GetAll()
        {
            return await unitOfWork.Todo.GetAll();
        }
    }
}
