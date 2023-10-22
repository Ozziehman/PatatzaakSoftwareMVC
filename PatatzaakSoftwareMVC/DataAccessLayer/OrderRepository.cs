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

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.orders.FindAsync(id);
        }

        /// <summary>
        /// add an order to the database
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Order> AddOrderAsync(Order order)
        {
            _context.orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        /// <summary>
        /// update an order in the database
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Order> UpdateOrderAsync(Order order)
        {
            var orderToUpdate = await _context.orders.FindAsync(order.Id);
            orderToUpdate.TotalPrice = order.TotalPrice;
            orderToUpdate.TimePlaced = order.TimePlaced;
            orderToUpdate.Finished = order.Finished;
            orderToUpdate.Status = order.Status;
            orderToUpdate.UserId = order.UserId;

            await _context.SaveChangesAsync();
            return order;
        }

        /// <summary>
        /// Delete an order by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
