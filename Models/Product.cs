using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductsAndCategories.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get;set;}

        [Required(ErrorMessage="Name field required")]
        [Display(Name="Name:")]
        public string Name {get;set;}

        [Required(ErrorMessage="Description field required")]
        [Display(Name="Description:")]
        public string Description {get;set;}

        [Required(ErrorMessage="Price field required")]
        [Display(Name="Price:")]
        public double? Price {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        public List<Association> ProductAssociations {get;set;}
    }
}