﻿using Microsoft.EntityFrameworkCore;
using static CashDesk.Model;

namespace CashDesk
{
    class CashDeskDataContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Deposit> Deposits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("CashDesk");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // NOTE creation of unique index. For details see
            // https://docs.microsoft.com/en-us/ef/core/modeling/indexes
            modelBuilder.Entity<Member>()
                .HasIndex(m => m.LastName)
                .IsUnique();

            // NOTE cascade delete. For details see
            // https://docs.microsoft.com/en-us/ef/core/saving/cascade-delete
            modelBuilder.Entity<Member>()
                .HasMany(m => m.Memberships)
                .WithOne(m => m.Member)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Membership>()
                .HasMany(m => m.Deposits)
                .WithOne(m => m.Membership)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
