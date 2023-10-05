using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;

namespace PatatzaakSoftwareMVC.DataAccessLayer
{
    public class ItemRepository
    {
        private readonly MainDb _context;

        public ItemRepository(MainDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _context.items.ToListAsync();
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _context.items.FindAsync(id);
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            _context.items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _context.items.FindAsync(id);

            if (item == null)
            {
                return false;
            }

            _context.items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
