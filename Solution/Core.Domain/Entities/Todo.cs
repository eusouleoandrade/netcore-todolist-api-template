using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class Todo : BaseEntity<int>
    {
        public string Title { get; private set; }

        public bool Done { get; private set; }

        public Todo(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;
        }

        public Todo(string title, bool done) : this(default, title, done)
        {
        }
    }
}