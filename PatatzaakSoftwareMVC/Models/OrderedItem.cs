using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatatzaakSoftware.Models
{
    public class OrderedItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public void AddToOrder()
        {

        }
    }
}