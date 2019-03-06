using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductsAndCategories.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get;set;}

        [Required(ErrorMessage="Name field required")]
        [Display(Name="Name:")]
        public string Name {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        public List<Association> CategoryAssociations {get;set;}
    }
}