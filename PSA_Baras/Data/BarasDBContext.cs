using Microsoft.EntityFrameworkCore;
using PSA_Baras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSA_Baras.Data
{
    public class BarasDBContext : DbContext
    {
        public BarasDBContext(DbContextOptions<BarasDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<BankAccount> BankAccount { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<CartItem> CartItem { get; set; }

        public DbSet<Cocktail> Cocktail { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Product> Product { get; set; }
    }
}
