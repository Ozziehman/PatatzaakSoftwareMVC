using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatatzaakSoftwareMVC.Models
{
    public class OrderedItem
    {
        public int Id { get; set; }

        [Required]   
        public Item? Item { get; set; }

        [Required]
        public Order? Order { get; set; }

































        //Old testing code with other DBContext method
        /*public OrderedItem ConvertItemToOrderedItem(int itemId, int orderId)
        {
            
            OrderedItem orderedItem = new OrderedItem();
            using (var dbContext = new MainDb())

            {
                orderedItem.Item = dbContext.items.Where(i => i.Id == itemId).FirstOrDefault();
                orderedItem.Order = dbContext.orders.Where(o => o.Id == orderId).FirstOrDefault();
                dbContext.orderedItems.Add(orderedItem);
                dbContext.SaveChanges();            
                return orderedItem;
            }
        }*/
    }
}