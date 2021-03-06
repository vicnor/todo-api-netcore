﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApi.Core.Models;

namespace TodoWebApi.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAll();
        Task<Todo> Add(Todo entity);
        Task<Todo> GetById(int id);
        Task Update(Todo todoForUpdate, Todo todo);
        Task Remove(Todo todo);
    }
}
