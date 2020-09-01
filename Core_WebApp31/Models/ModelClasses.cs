﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core_WebApp31.Models
{

    public class Category
    {
        [Key]
        public int CategoryRowId { get; set; }
        [Required(ErrorMessage = "Category Id is Must")]
        [StringLength(20, ErrorMessage ="Category Id can be max 20 characters")]
        public string CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name is Must")]
        [StringLength(200, ErrorMessage = "Category Name can be max 200 characters")]
        public string CategoeyName { get; set; }
        [Required(ErrorMessage = "Base Price is Must")]
        public int BasePrice { get; set; }
        // Expected one-to-many relationship
        public ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        [Key] // Primary Identity Key
        public int ProductRowId { get; set; }
        [Required]
        [StringLength(20)]
        public string ProductId { get; set; }
        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }
        [Required]
        [StringLength(20)]
        public string Manufacturer { get; set; }
        [Required]
        public int Price { get; set; }
        [ForeignKey("CategoryRowId")] // Foreign Key
        public int CategoryRowId { get; set; } // CategoryRowId CategoryRowId1 (ForeignKey)
        // referential Integrity
        public Category Category { get; set; }

    }


     
}
