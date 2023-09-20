using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
        public int CreateItem(string Name, float Price, float Discount)
        {
            
            Item item = new()
            {
                Name = Name,
                Price = Price,
                Discount = Discount
            };

            using (var dbContext = new MainDb())
            {
                dbContext.items.Add(item);
                int result = dbContext.SaveChanges();

                return result;
            }
        }
        public string LoadItems()
        {
            using (var dbContext = new MainDb())
            {
                var itemsToLoad = dbContext.items.ToList(); // Load all items

                if (itemsToLoad.Count > 0)
                {
                    var jsonItems = JsonConvert.SerializeObject(itemsToLoad, Formatting.Indented); // Convert to JSON
                    return jsonItems; // Go to new page with JSON data
                }
                else
                {
                    return "No items found";
                }
            }
        }

        public List<Item> LoadItemsObject() 
        {

            using (MainDb dbContext = new MainDb())
            {
                var items = dbContext.items.ToList();
                return items;
            }
        }


        public string LoadItemById(int itemToLoadId)
        {
            using (var dbContext = new MainDb())
            {
                var itemToLoad = dbContext.items.Where(item => item.Id == itemToLoadId).FirstOrDefault();

                if (itemToLoad != null)
                {
                    var jsonItem = JsonConvert.SerializeObject(itemToLoad, Formatting.Indented); // Convert to json
                    return jsonItem; // Go to new page wit json data
                }
                else
                {
                    Console.WriteLine("Item not found");
                    return "Not Items Found";
                }
            }
        }
 
        public int EditItemById(int itemToEditId, string? NewName, float NewPrice, float NewDiscount)
        {
            using (var dbContext = new MainDb())
            {
                var itemToEdit = dbContext.items.Where(item => item.Id == itemToEditId).FirstOrDefault();

                if (itemToEdit != null)
                {
                    //THIS IS AN EXAMPLE EDIT
                    if(!NewName.IsNullOrEmpty())
                    {
                        itemToEdit.Name = NewName;
                    }
                    if(NewPrice != 0)
                    {
                        itemToEdit.Price = NewPrice;
                    }
                    if(NewDiscount != 0)
                    {
                        itemToEdit.Discount = NewDiscount;
                    }

                    int result = dbContext.SaveChanges();
                    return result;
                }
                else
                {
                    return 0;
                }
            }

        }
        public int DeleteItemById(int itemToDeleteId)
        {
            using (var dbContext = new MainDb())
            {
                var itemToDelete = dbContext.items.Where(item => item.Id == itemToDeleteId).FirstOrDefault();

                if (itemToDelete != null)
                {
                    dbContext.items.Remove(itemToDelete);

                    int result = dbContext.SaveChanges();
                    return result;
                }
                else
                {
                    return 0;
                }
            }

        }
    }
}
