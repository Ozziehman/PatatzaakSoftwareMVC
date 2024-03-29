﻿using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatatzaakSoftwareMVC.Models
{
    public class OrderedItem
    {
        public int Id { get; set; }

        [Required]   
        public Item? Item { get; set; }
        public int ItemId { get; set; }
        [Required]
        public Order? Order { get; set; }
        public int OrderId { get; set; }
    }
}