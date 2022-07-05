using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Products.Models;
using System;

namespace Products.Data
{
    public class AppDbContext:DbContext
{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(b => b.Name).IsRequired();
        }
        public virtual DbSet<Product>? Products { get; set; }
    }
}
