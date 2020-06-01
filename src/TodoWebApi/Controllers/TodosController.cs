using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Core.Models;
using TodoWebApi.Services;

namespace TodoWebApi.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService todoService;

        public TodosController(ITodoService todoService)
        {
            this.todoService = todoService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Todo>> GetAll()
        {
            return await todoService.GetAll();
        }

        [HttpPost("")]
        public async Task<Todo> Add([FromBody] Todo entity)
        {
            var added = await todoService.Add(entity);
            return added;
        }
    }
}