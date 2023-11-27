using Microsoft.EntityFrameworkCore;
using ProductMarket.DataLayer.Entities;
using ProductMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMarket.DataLayer.Repositories
{
    public class ProductRepository
    {
        private MarketContext context;

        public ProductRepository()
        {
            context = new MarketContext();
        }

        public List<Product> GetProducts()
        {
            var products = context.Products.Include(p => p.Category).ToList();

            return products;
        }

        public int GetLastId()
        {
            return context.Products.OrderBy(p => p.Id).Last().Id;
        }

        public void Update(Product entityProduct)
        {
            var product = context.Products.Find(entityProduct.Id);

            product.Name = entityProduct.Name;
            product.Category.Name = entityProduct.Category.Name;
            product.Price = entityProduct?.Price;
            product.InStock = entityProduct?.InStock;

            context.Products.Update(product);
            context.SaveChanges();
        }

        public void Add(Product entityProduct)
        {
            context.Products.Add(entityProduct);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Products.Where(p => p.Id == id).ExecuteDelete();
        }

        public List<string> GetCategoriesNames()
        {
            var categoryNames = context.Categories.Select(c => c.Name).ToList();

            return categoryNames;
        }

        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategory(string name)
        {
            return context.Categories.SingleOrDefault(c => c.Name == name);
        }
    }
}
