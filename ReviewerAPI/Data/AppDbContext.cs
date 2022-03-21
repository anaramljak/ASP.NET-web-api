using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ReviewerDomain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ReviewerAPI
{
    public class AppDbContext : IdentityDbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=beautyReviewer;Integrated Security=True");
            }
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
