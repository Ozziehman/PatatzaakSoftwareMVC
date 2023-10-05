using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatatzaakSoftwareMVC.DataAccessLayer
{
    public class OrderRepository
    {
        private readonly MainDb _context;

        public OrderRepository(MainDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.orders.FindAsync(id);
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            _context.orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.orders.FindAsync(id);

            if (order == null)
                return false;

            _context.orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
