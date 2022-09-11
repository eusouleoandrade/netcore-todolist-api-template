using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class TodoList : BaseEntity<int>
    {
        public string Title { get; private set; }

        public bool Done { get; private set; }

        public TodoList(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;
        }

        public TodoList(string title, bool done) : this(default, title, done)
        {
        }
    }
}