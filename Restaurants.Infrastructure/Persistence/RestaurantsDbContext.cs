﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : DbContext(options)
    {

        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>()
                .OwnsOne(r => r.Address);
            modelBuilder.Entity<Restaurant>()
                .HasMany(d => d.Dishes)
                .WithOne()
                .HasForeignKey(d => d.RestaurantId);
        }
    }

}
