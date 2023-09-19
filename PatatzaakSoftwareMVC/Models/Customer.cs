using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatatzaakSoftwareMVC.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }

        public int? Points { get; set; }

        public ICollection<Order>? orders { get; set; }
        public ICollection<Voucher>? vouchers { get; set; }

        public Customer()
        {
            orders = new List<Order>();
            vouchers = new List<Voucher>();
        }
    }
}