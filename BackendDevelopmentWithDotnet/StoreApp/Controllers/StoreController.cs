using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

// refactoring and optimization assisted with copilot
namespace StoreApp.Controllers
{
    [ApiController]
    [Route("api/store")]
    public class StoreController : ControllerBase
    {
        private static readonly Dictionary<int, Item> _items = new();
        private static int _nextId = 0;

        [HttpGet("items")]
        public Ok<IEnumerable<Item>> GetItems() => TypedResults.Ok(_items.Values.AsEnumerable());

        [HttpPost("items/addnew")]
        public IResult AddItem(Item item)
        {
            if (_items.Values.Any(i => i.Equals(item)))
            {
                return TypedResults.Conflict(new { message = "Item already exists." });
            }

            int id = _nextId++;
            _items[id] = item;
            return TypedResults.Created(nameof(GetItems), item);
        }

        [HttpPut("items/update/{id:int}")]
        public IResult UpdateItem(int id, Item item)
        {
            if (!_items.ContainsKey(id))
            {
                return TypedResults.NotFound(new { message = "Item doesn't exist." });
            }

            _items[id] = item;
            return TypedResults.Ok(item);
        }

        [HttpDelete("items/delete/{id:int}")]
        public IResult DeleteItem(int id)
        {
            if (!_items.ContainsKey(id))
            {
                return TypedResults.NotFound(new { message = "Item doesn't exist." });
            }

            _items.Remove(id);
            return TypedResults.Ok();
        }
    }
}
