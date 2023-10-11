using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.DataAccessLayer;
using PatatzaakSoftwareMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderRepository _orderRepository;

    public OrderController(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await _orderRepository.GetOrdersAsync();
        return Ok(orders);
    }

    /// <summary>
    /// Get a single order by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    /// <summary>
    /// Creates an order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(Order order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newOrder = await _orderRepository.AddOrderAsync(order);
        return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
    }

    /// <summary>
    /// Update an order by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        var existingOrder = await _orderRepository.GetOrderByIdAsync(id);

        if (existingOrder == null)
        {
            return NotFound();
        }

        var updatedOrder = await _orderRepository.UpdateOrderAsync(order);

        if (updatedOrder == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete an order by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var result = await _orderRepository.DeleteOrderAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
