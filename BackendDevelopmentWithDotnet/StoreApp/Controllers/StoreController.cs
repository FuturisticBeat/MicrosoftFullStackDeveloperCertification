using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    [ApiController]
    [Route("api/store")]
    public class StoreController : ControllerBase
    {
        private static List<Item> _items = new List<Item>();

        [HttpGet("items")]
        public List<Item> GetItems() => _items;

        [HttpPost("items/addnew")]
        public IActionResult AddItem(Item item)
        {
            Item? existingItem = _items.FirstOrDefault();

            if (existingItem != null)
            {
                return Conflict(new { message = "Item already exists." });
            }

            _items.Add(item);
            return CreatedAtAction(nameof(GetItems), new { id = _items.Count - 1 }, item);
        }

        [HttpPut("items/update/{id:int}")]
        public IActionResult UpdateItem(int id, Item item)
        {
            if (id < 0 || id >= _items.Count)
            {
                return NotFound(new { message = "Item doesn't exist." });
            }
            
            _items[id] = item;
            return Ok(item);
        }

        [HttpDelete("items/delete/{id:int}")]
        public IActionResult DeleteItem(int id)
        {
            if (id < 0 || id >= _items.Count)
            {
                return NotFound(new { message = "Item doesn't exist." });
            }

            _items.RemoveAt(id);
            return Ok();
        }
    }
}
