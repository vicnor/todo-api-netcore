using Moq;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoWebApi.Controllers;
using TodoWebApi.Core.Models;
using TodoWebApi.Services;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace TodoWebApi.Tests
{
    public class TodosControllerTest
    {
        private IEnumerable<Todo> todos = new List<Todo>()
        {
            new Todo() { Id = 1, Title = "Test 1", Completed = false, Order = 1 },
            new Todo() { Id = 2, Title = "Test 2", Completed = true, Order = 2 }
        };

        [Fact]
        public async Task TestRemove()
        {
            // Setup
            int id = 1;
            var todo = new Todo() { Id = 1, Title = "Test 1", Completed = false, Order = 1 };

            var service = new Mock<ITodoService>();
            service.Setup(mock => mock.GetById(id)).ReturnsAsync(todo);
            service.Setup(mock => mock.Remove(todo)).Returns(Task.FromResult(default(object)));

            var controller = new TodosController(service.Object);

            // Act
            var result = await controller.Remove(id);

            // Assert
            service.Verify(v => v.GetById(id));
            service.Verify(v => v.Remove(todo));
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestRemove_NotFound()
        {
            // Setup
            int id = 1;
            var todo = new Todo() { Id = 1, Title = "Test 1", Completed = false, Order = 1 };

            var service = new Mock<ITodoService>();
            service.Setup(mock => mock.GetById(id)).ReturnsAsync((Todo) null);

            var controller = new TodosController(service.Object);

            // Act
            var result = await controller.Remove(id);

            // Assert
            service.Verify(v => v.GetById(id));
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestUpdate()
        {
            // Setup
            int id = 1;
            var updatedTitle = "updated";
            var todoForUpdate = new Todo() { Id = 1, Title = "Test 1", Completed = false, Order = 1 };
            var todo = new Todo() { Id = 1, Title = updatedTitle, Completed = false, Order = 1 };

            var service = new Mock<ITodoService>();
            var queue = new Queue<Todo>();
            queue.Enqueue(todoForUpdate);
            queue.Enqueue(todo);
            service.Setup(mock => mock.GetById(id)).ReturnsAsync(queue.Dequeue);
            service.Setup(mock => mock.Update(todoForUpdate, todo)).Returns(Task.FromResult(default(object)));

            var controller = new TodosController(service.Object);

            // Act
            var result = await controller.Update(id, todo);

            // Assert
            service.Verify(v => v.GetById(id));
            service.Verify(v => v.Update(todoForUpdate, todo));
            Assert.NotNull(result);
            Assert.Equal(todo, result.Value);
            Assert.Equal(updatedTitle, result.Value.Title);
        }

        [Fact]
        public async Task TestUpdate_NotFound()
        {
            // Setup
            int id = 1;
            var todo = new Todo() { Id = 1, Title = "updated", Completed = false, Order = 1 };

            var service = new Mock<ITodoService>();
            service.Setup(mock => mock.GetById(id)).ReturnsAsync((Todo) null);

            var controller = new TodosController(service.Object);

            // Act
            var result = await controller.Update(id, todo);

            // Assert
            service.Verify(v => v.GetById(id));
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task TestGetById()
        {
            // Setup
            int id = 1;
            var todo = todos.FirstOrDefault(t => t.Id == id);

            var service = new Mock<ITodoService>();
            service.Setup(mock => mock.GetById(id)).ReturnsAsync(todo);

            var controller = new TodosController(service.Object);

            // Act
            var result = await controller.GetById(id);

            // Assert
            service.Verify(v => v.GetById(id));
            Assert.NotNull(result);
            Assert.Equal(todo, result.Value);
        }

        [Fact]
        public async Task TestGetById_NotFound()
        {
            // Setup
            int id = 1;

            var service = new Mock<ITodoService>();
            service.Setup(mock => mock.GetById(id)).ReturnsAsync((Todo)null);

            var controller = new TodosController(service.Object);

            // Act
            var result = await controller.GetById(id);

            // Assert
            service.Verify(v => v.GetById(id));
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task TestGetAll()
        {
            // Setup
            var service = new Mock<ITodoService>();
            service.Setup(mock => mock.GetAll()).ReturnsAsync(todos);

            var controller = new TodosController(service.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            service.Verify(v => v.GetAll());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestAdd()
        {
            // Setup
            int id = 1;
            var todo = todos.FirstOrDefault(t => t.Id == id);

            var service = new Mock<ITodoService>();
            service.Setup(mock => mock.Add(todo)).ReturnsAsync(todo);
            
            var controller = new TodosController(service.Object);

            // Act
            var result = await controller.Add(todo);

            // Assert
            service.Verify(v => v.Add(todo));
            Assert.NotNull(result);
            Assert.Equal(todo, result);
        }
    }
}
