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

    public async Task<OrderedItem> GetOrderedItemByIdAsync(int id)
    {
        return await _context.orderedItems.FindAsync(id);
    }

    public async Task<OrderedItem> AddOrderedItemAsync(OrderedItem orderedItem)
    {
        _context.orderedItems.Add(orderedItem);
        await _context.SaveChangesAsync();
        return orderedItem;
    }

    public async Task<OrderedItem> UpdateOrderedItemAsync(OrderedItem orderedItem)
    {
        _context.Entry(orderedItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return orderedItem;
    }

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