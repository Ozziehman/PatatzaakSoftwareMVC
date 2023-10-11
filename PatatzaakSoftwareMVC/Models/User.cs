using System.ComponentModel.DataAnnotations;

namespace PatatzaakSoftwareMVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Password { get; set; }
        public int Points { get; set; }
        public bool IsAdmin { get; set; }  
        public ICollection<Order>? Orders { get; set; } 
        public ICollection<Voucher>? Vouchers { get; set; }

        public string UserDisplay => $"Email: '{Email}' - UserID: '{Id}'";
    }
}
