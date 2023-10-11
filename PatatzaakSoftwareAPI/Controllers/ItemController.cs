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
    
    /// <summary>
    /// Get a single item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    // POST: api/Api
    /// <summary>
    /// Create an item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Updates an item
    /// </summary>
    /// <param name="id"></param>
    /// <param name="item"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Delete item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var result = await _itemRepository.DeleteItemAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}