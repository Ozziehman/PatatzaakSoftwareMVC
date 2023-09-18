using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatatzaakSoftwareMVC.Models
{
    public class Voucher
    {
        public int Id { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Code { get; set; }

        [Required]
        public DateTime ExpiresBy { get; set; }

        [Required]
        public Customer? Customer { get; set; }
    }
}