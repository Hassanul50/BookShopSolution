﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100,ErrorMessage ="Display Order mast be 1-100")]
        public int DisplayOrder { get; set; }
        //public int Id { get; set; }
        //public int Id { get; set; }
        //public int Id { get; set; }
    }
}
