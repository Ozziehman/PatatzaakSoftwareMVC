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

        [Required]
        public Customer? Customer { get; set; }
        public ICollection<OrderedItem>? OrderedItems { get; set; }

        public Order()
        {
            OrderedItems = new List<OrderedItem>();
        }   
       
    }
}