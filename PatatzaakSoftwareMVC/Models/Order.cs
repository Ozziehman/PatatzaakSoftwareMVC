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

        public void AddOrderedItem(OrderedItem orderedItem)
        {
            orderedItem.Order = this;

        }
        public void GetOrderedItems()
        {

        }
        public void UseVoucher()
        {

        }
        public void CreateOrder()
        {

        }
        public void loadOrders()
        {

        }
        public void LoadOrderById()
        {

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

        
    }
}