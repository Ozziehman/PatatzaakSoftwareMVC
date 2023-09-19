using PatatzaakSoftwareMVC.Data;
using System.ComponentModel.DataAnnotations;

namespace PatatzaakSoftwareMVC.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        [MaxLength(255)]
        public string ImagePath { get; set; } = "../Resources/Images/placeholder.jpg";

        [Required]
        public float Price { get; set; }

        public float? Discount { get; set; }

        public void CreateOrderedItem()
        {

        }
        public void AddDiscount()
        {

        }
        public void CreateItem()
        {

        }
        public void LoadItems()
        {

        }
        public void LoadItemById()
        {

        }
        public void EditItemById()
        {

        }
        public void DeleteItemById()
        {

        }
    }
}
