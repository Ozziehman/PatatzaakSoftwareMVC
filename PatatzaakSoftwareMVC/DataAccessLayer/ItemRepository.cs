﻿using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Get single item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _context.items.FindAsync(id);
        }

        /// <summary>
        /// Add an item to the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Item> AddItemAsync(Item item)
        {
            _context.items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        /// <summary>
        /// Update an item in the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Item> UpdateItemAsync(Item item)
        {
            var itemToUpdate = await _context.items.FindAsync(item.Id);

            itemToUpdate.Name = item.Name;
            itemToUpdate.Price = item.Price;
            itemToUpdate.ImagePath = item.ImagePath;
            itemToUpdate.Discount = item.Discount;

            await _context.SaveChangesAsync();
            return item;
        }

        /// <summary>
        /// Delete an item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
