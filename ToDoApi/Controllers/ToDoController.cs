using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Data;
using ToDoApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApi.Controllers
{
    [Route("api/ToDo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;
        }

        // GET api/ToDo
        [HttpGet]
        public ActionResult<List<ToDoItem>> GetAll()
        {
            return _context.ToDoItems.ToList();
        }

        // GET api/ToDo/5
        [HttpGet("{id}", Name = "GetToDo")]
        public ActionResult<ToDoItem> GetById(int id)
        {
            var item = _context.ToDoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST api/ToDo
        [HttpPost]
        public async Task<IActionResult> Create(ToDoItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.ToDoItems.AddAsync(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDo", new { id = item.Id }, item);
        }

        // PUT api/ToDo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ToDoItem item)
        {
            var todo = _context.ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Text = item.Text;

            _context.ToDoItems.Update(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = _context.ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
