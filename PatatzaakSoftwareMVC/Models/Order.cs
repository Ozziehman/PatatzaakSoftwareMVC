using Humanizer.Localisation.TimeToClockNotation;
using PatatzaakSoftwareMVC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatatzaakSoftwareMVC.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public float TotalPrice { get; set; }

        [Required]
        public DateTime TimePlaced { get; set; } = DateTime.Now;

        /// <summary>
        /// Finished is not neccesary but still left in for later use maybe
        /// </summary>
        public bool Finished { get; set; }
        public string? Status { get; set; } = "BeingChosen"; //1: BeingChosen
                                                             //2: Placed
                                                             //3: Ready
                                                             //4: Completed    
        
        public User? User { get; set; }
        public int UserId { get; set; }
        public ICollection<OrderedItem>? OrderedItems { get; set; }

        public Order()
        {
            OrderedItems = new List<OrderedItem>();
        }
    }
}