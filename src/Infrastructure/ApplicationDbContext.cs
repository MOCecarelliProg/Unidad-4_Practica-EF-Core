using Application.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Category>().HasData(CreateCategoryDataSeed()
                );

            modelBuilder.Entity<Product>().HasData(CreateProductDataSeed()
                );

            base.OnModelCreating(modelBuilder);
        }

        private Category[] CreateCategoryDataSeed()
        {
            Category[] categories = [
                    new Category
                    {
                        Id = 1,
                        Name = "Temporada Otoño/Invierno",
                        Description = "Camperas, Guantes, Gorros de lana, Bufandas, etc.",
                        Removed = false
                    },
                    new Category {
                        Id = 2,
                        Name = "Temporada Primavera/Verano",
                        Description = "Camisas, Remeras, Shorts, Ojotas, etc.",
                        Removed = false
                    },
                    new Category {
                        Id = 3,
                        Name = "Accesorios",
                        Description = "Lentes, carteras, bolsos, mochilas, etc.",
                        Removed = false
                    },
            ];

            return categories;
        }

        private Product[] CreateProductDataSeed()
        {
            Product[] products = [
                new Product
                {
                    Id = 1,
                    Name = "Guantes de cuero",
                    Price = 19000m,
                    Description = "Guantes de cuero vacuno negro",
                    CategoryId = 1,
                    Removed = false
                },
                new Product
                {
                    Id = 2,
                    Name = "Guantes de lana",
                    Price = 9500m,
                    Description = null,
                    CategoryId = 1,
                    Removed = false
                },
                new Product
                {
                    Id = 3,
                    Name = "Camisa floreada",
                    Price = 17000m,
                    Description = null,
                    CategoryId = 2,
                    Removed = false
                },
            ];

            return products;
        }
    }
}
