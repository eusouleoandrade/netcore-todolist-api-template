﻿using Core.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    [Table("Todo")]
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

        public Todo(Int64 id, string title, string done)
        {
            Id = (int)id;
            Title = title;
            Done = bool.TryParse(done, out bool result) && result;
        }

        public Todo(string title, bool done) : this(default, title, done)
        {
        }
    }
}