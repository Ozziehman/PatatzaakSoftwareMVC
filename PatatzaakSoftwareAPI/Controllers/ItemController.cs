using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.Models;
using PatatzaakSoftwareMVC.DataAccessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly ItemRepository _itemRepository;

    public ItemController(ItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    // GET: api/Api
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems()
    {
        var items = await _itemRepository.GetItemsAsync();
        return Ok(items);
    }

    // GET: api/Api/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItem(int id)
    {
        var item = await _itemRepository.GetItemByIdAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    //POST, PUT and DELETE have been disabled as this API is not intended to be used for CRUD operations just for reading data.

    /* // POST: api/Api
     [HttpPost]
     public async Task<ActionResult<Item>> PostItem(Item item)
     {
         if (!ModelState.IsValid)
         {
             return BadRequest(ModelState);
         }

         var newItem = await _itemRepository.AddItemAsync(item);
         return CreatedAtAction(nameof(GetItem), new { id = newItem.Id }, newItem);
     }

     // PUT: api/Api/5
     [HttpPut("{id}")]
     public async Task<IActionResult> PutItem(int id, Item item)
     {
         if (id != item.Id)
         {
             return BadRequest();
         }

         var existingItem = await _itemRepository.GetItemByIdAsync(id);

         if (existingItem == null)
         {
             return NotFound();
         }

         var updatedItem = await _itemRepository.UpdateItemAsync(item);

         if (updatedItem == null)
         {
             return NotFound();
         }

         return NoContent();
     }

     // DELETE: api/Api/5
     [HttpDelete("{id}")]
     public async Task<IActionResult> DeleteItem(int id)
     {
         var result = await _itemRepository.DeleteItemAsync(id);

         if (!result)
         {
             return NotFound();
         }

         return NoContent();
     }*/
}