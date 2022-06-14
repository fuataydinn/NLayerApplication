using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)  //Basedeki options'a yolluyoruz
        {
        //Bu optionsla beraber veri tabanı yolunu startup'ta vermek icin bu constructor'ı olusturmamız gerekiyordu

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tum configuration dosyalarını reflextion ile burada tek seferde uyguluyoruz.
            //Bunu da IEntityTypeConfiguration<Category> seklinde kalıtım alan sınıfları bularak yapıyor
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature() { Id = 1, Color = "Kırmızı", Height = 100, Width = 200,ProductId=1 },
                new ProductFeature() { Id = 2, Color = "Mavi", Height = 300, Width = 200,ProductId=2 }
                      
                );  //istersek buradan da seed data olusturabiliriz'i gormek icin bunu yaptık

            base.OnModelCreating(modelBuilder);
        }
    }
}
