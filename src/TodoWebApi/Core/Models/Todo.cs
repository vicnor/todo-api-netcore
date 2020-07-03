
namespace TodoWebApi.Core.Models
{
    public class Todo : Entity<int>
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }

    }
}
