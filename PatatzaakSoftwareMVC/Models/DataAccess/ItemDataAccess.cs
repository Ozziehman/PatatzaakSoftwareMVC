
using PatatzaakSoftwareMVC.Models.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatatzaakSoftwareMVC.Models.DataAccess
{
    public static class ItemDataAccess
    {
        public static string AddItem()
        {
            Item newItem = new Item("Patat", "../Resources/Images/placeholder.jpg", 1.50f, null);   

            return $"added item {newItem.Name}";

            //THIS IS AN EXAMPLE OF HOW TO USE THE DATA ACCESS LAYER
        }

        public static void RemoveItem()
        {

        }
        public static void UpdateItem()
        {

        }
        public static void GetAllItems()
        {

        }
        public static void GetItemById()
        {

        }
    }
}
