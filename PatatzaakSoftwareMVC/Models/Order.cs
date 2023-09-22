using PatatzaakSoftwareMVC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatatzaakSoftwareMVC.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public float TotalPrice { get; set; }

        [Required]
        public DateTime TimePlaced { get; set; }

        public bool Finished { get; set; }

        public User? user { get; set; }
        public ICollection<OrderedItem>? OrderedItems { get; set; }

        public Order()
        {
            OrderedItems = new List<OrderedItem>();
        }






































        //Old testing code with other DBContext method
        /*public void AddOrderedItem(OrderedItem orderedItem)
        {
            orderedItem.Order = this;

        }
        public void GetOrderedItems()
        {

        }
        public void UseVoucher()
        {

        }
        public int CreateOrder()
        {
            using(var dbContext = new MainDb())
            {
                Order order = new Order();
                dbContext.orders.Add(order);
                dbContext.SaveChanges();
                return order.Id;
            }
        }
        public void loadOrders()
        {

        }
        public Order LoadOrderById(int Id)
        {
            using (var dbContext = new MainDb())
            {
                Order order = dbContext.orders.Find(Id);
                if (order == null)
                {
                    Console.WriteLine("No order found that matches the current ordersessionId");
                    return null;
                }
                else
                {
                    return order;
                }
            }
        }
        public void EditOrderById()
        {

        }
        public void DeleteOrderById()
        {

        }
        public void CompleteOrder()
        {

        }
*/

    }
}