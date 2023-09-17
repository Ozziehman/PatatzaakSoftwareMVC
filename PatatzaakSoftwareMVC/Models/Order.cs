using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatatzaakSoftware.Models
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
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        

        public void GetItems()
        {

        }

        public void UseVoucher()
        {

        }

        public void CreateOrder()
        {

        }

        public void GetOrderById()
        {

        }

        public void GetTotalPrice()
        {

        }
    }
}