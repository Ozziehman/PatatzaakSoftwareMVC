using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.DataAccessLayer;
using PatatzaakSoftwareMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class OrderedItemController : ControllerBase
{
    private readonly OrderedItemRepository _orderedItemRepository;

    public OrderedItemController(OrderedItemRepository orderedItemRepository)
    {
        _orderedItemRepository = orderedItemRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderedItem>>> GetOrderedItems()
    {
        var orderedItems = await _orderedItemRepository.GetOrderedItemsAsync();
        return Ok(orderedItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderedItem>> GetOrderedItem(int id)
    {
        var orderedItem = await _orderedItemRepository.GetOrderedItemByIdAsync(id);

        if (orderedItem == null)
        {
            return NotFound();
        }

        return Ok(orderedItem);
    }

    //POST, PUT and DELETE have been disabled as this API is not intended to be used for CRUD operations just for reading data.
    /* [HttpPost]
     public async Task<ActionResult<OrderedItem>> PostOrderedItem(OrderedItem orderedItem)
     {
         var newOrderedItem = await _orderedItemRepository.AddOrderedItemAsync(orderedItem);
         return CreatedAtAction(nameof(GetOrderedItem), new { id = newOrderedItem.Id }, newOrderedItem);
     }

     [HttpPut("{id}")]
     public async Task<IActionResult> PutOrderedItem(int id, OrderedItem orderedItem)
     {
         if (id != orderedItem.Id)
         {
             return BadRequest();
         }

         var updatedOrderedItem = await _orderedItemRepository.UpdateOrderedItemAsync(orderedItem);

         if (updatedOrderedItem == null)
         {
             return NotFound();
         }

         return NoContent();
     }

     [HttpDelete("{id}")]
     public async Task<IActionResult> DeleteOrderedItem(int id)
     {
         var result = await _orderedItemRepository.DeleteOrderedItemAsync(id);

         if (!result)
         {
             return NotFound();
         }

         return NoContent();
     }*/
}