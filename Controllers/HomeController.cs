using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//added
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;
using System.Linq;

namespace ProductsAndCategories.Controllers
{
    public class HomeController : Controller
    {
        private Context dbContext;
     
        // here we can "inject" our context service into the constructor
        public HomeController(Context context)
        {
            dbContext = context;
        }

// ============================================= All Products Route
        [HttpGet("products")]
        public IActionResult Products()
        {
            //new product <list>. Show All from db.
            ViewModel products = new ViewModel();
            products.Products = dbContext.Products.ToList();
            return View("Products", products);
        }

// ============================================= POST New Product Route
        [HttpPost("products")]
        public IActionResult AddProduct(Product product)
        {
            //Validations => Save into db
            if(ModelState.IsValid)
            {
                dbContext.Add(product);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
            }
            return View("Products");
        }

// ============================================= All Categories Route
    [HttpGet("categories")]
    public IActionResult Categories()
    {
        //new categories <list>. Show All from db.
        ViewModel categories = new ViewModel();
        categories.Categories = dbContext.Categories.ToList();
        return View(categories);
    }
// ============================================= POST New Category Route
    [HttpPost("categories")]
    public IActionResult AddCategories(Category category)
    {
            //Validations => Save into db
            if(ModelState.IsValid)
            {
                dbContext.Add(category);
                dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View("Categories");
    }

// ============================================= Each Product route
    [HttpGet("/products/{id}")]
    public IActionResult ProductDetails(int id)
    {
        ViewModel product = new ViewModel();

        product.Product = dbContext.Products
        .Include(p => p.ProductAssociations)
        .ThenInclude(a=> a.Category)
        .FirstOrDefault(p => p.ProductId == id);
        //Add Categories to the list from DB
        product.Categories = dbContext.Categories.ToList();

        //Removes the Category ID  from the list after it was selected once
        product.ProductInCategories = dbContext.Categories
        .Where(c => c.CategoryAssociations.All(ca => ca.ProductId != id)).ToList();

        return View(product);
    }
// ============================================= Add Category to Product
    [HttpPost("products/category")]
    public IActionResult AddCategoryToProduct(Association association)
    {
        System.Console.WriteLine($"\n\n\nCategory Id:{association.CategoryId} Product Id: {association.ProductId}");
        dbContext.Add(association);
        dbContext.SaveChanges();
        return Redirect($"/products/{association.ProductId}");
    }

// ============================================= Each Category route
    [HttpGet("category/{id}")]
    public IActionResult CategoryDetails(int id)
    {
        ViewModel category = new ViewModel();

        category.Category = dbContext.Categories
        .Include(ca => ca.CategoryAssociations)
        .ThenInclude(a => a.Product)
        .FirstOrDefault(c => c.CategoryId == id);
        //Add Products to the list from DB
        category.Products = dbContext.Products.ToList();

        //Removes the Product ID  from the list after it was selected once
        category.CategoriesInProducts = dbContext.Products
        .Where(p => p.ProductAssociations
        .All(prod => prod.CategoryId != id))
        .ToList();

        return View(category);
    }

// ============================================= Add Product to Category

        [HttpPost("category/product")]
        public IActionResult CategoryToProduct(Association association)
        {
            dbContext.Associations.Add(association);
            dbContext.SaveChanges();
            return Redirect($"/category/{association.CategoryId}");
        }





    }
}
