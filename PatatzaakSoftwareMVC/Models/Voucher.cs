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
        public int Price { get; set; } //To be bought with points

        public float VoucherDiscount { get; set; }

        [Required]
        [MaxLength(255)]
        public string? VoucherCode { get; set; }

        public string VoucherDisplay => $"{VoucherCode} - {VoucherDiscount}%";

        [Required]
        public DateTime ExpiresBy { get; set; }

        [Required]
        public User? User { get; set; }
        public int? UserId { get; set; }      
    }
}