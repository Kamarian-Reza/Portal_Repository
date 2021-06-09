using Microsoft.EntityFrameworkCore;
using Portal_Project.Models.Portal.DMC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_Project.Models.Portal
{
    public static class ModelBuilderExtensionMethods
    {
        public static void Relations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .HasOne<ApplicationUser>(p => p.ApplicationUser)
                        .WithMany(au => au.Products)
                        .HasForeignKey(p => p.UserID)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bid>()
                        .HasOne<ApplicationUser>(b => b.ApplicationUser)
                        .WithMany(au => au.Bids)
                        .HasForeignKey(b => b.UserID)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bid>()
                        .HasOne<Product>(b => b.Product)
                        .WithMany(p => p.Bids)
                        .HasForeignKey(b => b.ProductID)
                        .OnDelete(DeleteBehavior.Restrict);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
        }
    }
}