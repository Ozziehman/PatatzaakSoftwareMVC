﻿using Microsoft.EntityFrameworkCore.Storage;
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

        public float Discount { get; set; }
    }
}
