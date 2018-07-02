using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsComplete { get; set; }
    }
}
