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

        public int CreateOrderedItem(Item item )
        {
            using(var dbContext = new MainDb())
            {
                OrderedItem orderedItem = new OrderedItem();
                orderedItem.Item = item;
                dbContext.orderedItems.Add(orderedItem);
                int result = dbContext.SaveChanges();
                return result;
            }
        }


    }
}