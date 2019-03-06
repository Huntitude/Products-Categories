using System.Collections.Generic;
namespace ProductsAndCategories.Models
{
    public class ViewModel
    {
        public List<Product> Products {get;set;}
        public List<Category> Categories {get;set;}
        public Product Product {get;set;}
        public Category Category {get;set;}
        public Association Association {get;set;}
        public List<Category> ProductInCategories  {get;set;}
        public List<Product> CategoriesInProducts {get;set;}
    }
}