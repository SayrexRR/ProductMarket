using Microsoft.EntityFrameworkCore;
using ProductMarket.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProductMarket.Entities
{
    public class MarketContext : DbContext
    {
        public MarketContext()
        {
            Database.EnsureCreated();

            if (!Products.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Овочі та фрукти" },
                    new Category { Name = "Бакалія" },
                    new Category { Name = "Молочні продукти" },
                    new Category { Name = "Хлібобулочні вироби" },
                    new Category { Name = "Напої" },
                    new Category { Name = "Кондитерські вироби" }
                };

                Categories.AddRange(categories);

                var products = new List<Product>
                {
                    new Product { Name = "Капуста", Category = categories[0], Price = 8.25M, InStock = 500 },
                    new Product { Name = "Мак. Вир. 420 г La Pasta Спіраль", Category = categories[1], Price = 37.30M, InStock = 100 },
                    new Product { Name = "Мак.вир. 0,5кг De Luxe Foods&Goods Selected Локшина", Category = categories[1], Price = 62.40M, InStock = 110 },
                    new Product { Name = "Банан", Category = categories[0], Price = 61.95M, InStock = 1017 },
                    new Product { Name = "Авокадо вагове 1 гат", Category = categories[0], Price = 183.99M, InStock = 200 },
                    new Product { Name = "Олія 0,5л Своя лінія соняшникова рафінована", Category = categories[1], Price = 29.97M, InStock = 354 },
                    new Product { Name = "Йогурт 0,8 кг Активіа Ананас 1,5% п/бут", Category = categories[2], Price = 61.90M, InStock = 180 },
                    new Product { Name = "Батончик 57г Mars Bounty", Category = categories[5], Price = 21.60M, InStock = 1700 },
                    new Product { Name = "Напій 1 л Coca-Cola безалкoгoльний сильнoгазoваний", Category = categories[4], Price = 27.60M, InStock = 700 },
                    new Product { Name = "Сік 0,95 Jaffa Яблуко тетpa-пaк", Category = categories[4], Price = 53.90M, InStock = 425 },
                    new Product { Name = "Цукерки 90 г МІЛКА кульки з молочного шоколаду з начинкою м/уп", Category = categories[5], Price = 86.60M, InStock = 160 },
                };

                Products.AddRange(products);

                SaveChanges();
            }
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=MarketDb;Trusted_Connection=True;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.Property(p => p.Name)
                .HasMaxLength(250);

                e.ToTable(p => p.HasCheckConstraint("CK_Prices", "[Price] >= 0"));

                e.ToTable(p => p.HasCheckConstraint("CK_InStock", "[InStock] >= 0"));

                e.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.Property(c => c.Name)
                .HasMaxLength(100);
            });
        }
    }
}
