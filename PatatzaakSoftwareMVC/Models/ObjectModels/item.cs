using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatatzaakSoftwareMVC.Models.ObjectModels
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
        [MaxLength(255)]
        public string ImagePath { get; set; } = "../Resources/Images/placeholder.jpg";
        [Required]
        public float Price { get; set; }
        public float? Discount { get; set; }

        public Item(string name, string imagePath, float price, float? discount)
        {
            Name = name;
            ImagePath = imagePath;
            Price = price;
            Discount = discount;
        }

        public void CreateOrderedItem()
        {

        }

        public void AddDiscount()
        {

        }

    }
}