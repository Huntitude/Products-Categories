// forms the relationship between our models and the database

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace ProductsAndCategories.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Product> Products {get;set;}
        public DbSet<Category> Categories {get;set;}
        public DbSet<Association> Associations {get;set;}
    }
}