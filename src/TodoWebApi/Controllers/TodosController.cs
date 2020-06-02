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
        public async Task<Todo> Add([FromBody] Todo todo)
        {
            var added = await todoService.Add(todo);
            return added;
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<Todo>> GetById(int id)
        {
            var todo = await todoService.GetById(id);

            if (todo == null)
            {
                return NotFound("Could not find todo with id " + id);
            }

            return todo;
        }

        [HttpPut("/{id}")]
        public async Task<ActionResult<Todo>> Update(int id, [FromBody] Todo todo)
        {
            var todoForUpdate = await todoService.GetById(id);

            if(todoForUpdate == null)
            {
                return NotFound("Could not find todo with id " + id);
            }

            await todoService.Update(todoForUpdate, todo);

            var updatedTodo = await todoService.GetById(id);
            return updatedTodo;
        }

        [HttpDelete("/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var todo = await todoService.GetById(id);

            if (todo == null)
            {
                return NotFound("Could not find todo with id " + id);
            }

            await todoService.Remove(todo);

            return NoContent();
        }
    }
}