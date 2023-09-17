using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatatzaakSoftwareMVC.Models.ObjectModels
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }
        public int? Points { get; set; }


        public void PlaceOrder()
        {

        }
        public void GetAllOrders()
        {

        }
        public void GetOrderById()
        {

        }
    }
}