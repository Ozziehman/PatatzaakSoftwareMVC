using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OrderedItemRepository
{
    private readonly MainDb _context;

    public OrderedItemRepository(MainDb context)
    {
        _context = context;
    }

    public async Task<List<OrderedItem>> GetOrderedItemsAsync()
    {
        return await _context.orderedItems.ToListAsync();
    }

    /// <summary>
    /// Get an ordered item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<OrderedItem> GetOrderedItemByIdAsync(int id)
    {
        return await _context.orderedItems.FindAsync(id);
    }

    /// <summary>
    /// Add an ordered item to the database
    /// </summary>
    /// <param name="orderedItem"></param>
    /// <returns></returns>
    public async Task<OrderedItem> AddOrderedItemAsync(OrderedItem orderedItem)
    {
        _context.orderedItems.Add(orderedItem);
        await _context.SaveChangesAsync();
        return orderedItem;
    }

    /// <summary>
    /// Update an ordered item in the database
    /// </summary>
    /// <param name="orderedItem"></param>
    /// <returns></returns>
    public async Task<OrderedItem> UpdateOrderedItemAsync(OrderedItem orderedItem)
    {
        _context.Entry(orderedItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return orderedItem;
    }

    /// <summary>
    /// Delete an ordered item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteOrderedItemAsync(int id)
    {
        var orderedItem = await _context.orderedItems.FindAsync(id);
        if (orderedItem == null)
            return false;

        _context.orderedItems.Remove(orderedItem);
        await _context.SaveChangesAsync();
        return true;
    }
}