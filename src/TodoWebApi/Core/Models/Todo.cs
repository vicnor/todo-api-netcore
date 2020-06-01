
namespace TodoWebApi.Core.Models
{
    public class Todo : Entity<int>
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }

        public override bool Equals(object obj)
        {
            var todo = obj as Todo;
            return (todo != null) && (Id == todo.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
