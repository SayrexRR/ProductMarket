using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductMarket.BussinessLayer.Models;
using ProductMarket.DataLayer.Repositories;

namespace ProductMarket.BussinessLayer.Services
{
    public class ProductService
    {
        private readonly ProductRepository repository;

        public ProductService()
        {
            repository = new ProductRepository();
        }

        public List<Product> GetProducts()
        {
            var products = repository.GetProducts();

            return products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category.Name,
                Price = p.Price,
                InStock = p.InStock,
            }).ToList();
        }

        public List<Category> GetCategories()
        {
            var categories = repository.GetCategories();

            return categories.Select(c => new Category
            {
                Name = c.Name
            }).ToList();
        }

        public List<string> GetCategoriesName()
        {
            var categoriesName = repository.GetCategoriesNames();

            return categoriesName;
        }

        public int GetLastId()
        {
            return repository.GetLastId();
        }

        public void Update(Product modelProduct)
        {
            repository.Update(new DataLayer.Entities.Product
            {
                Id = modelProduct.Id,
                Name = modelProduct.Name,
                Category = repository.GetCategory(modelProduct.CategoryName),
                Price = modelProduct.Price,
                InStock = modelProduct.InStock,
            });
        }

        public void Add(Product modelProduct)
        {
            repository.Add(new DataLayer.Entities.Product
            {
                Name = modelProduct.Name,
                Category = repository.GetCategory(modelProduct.CategoryName),
                Price = modelProduct.Price,
                InStock = modelProduct.InStock,
            });
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
