using Events.Areas.Identity.Data;
using Events.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Data
{
    public class EventsDbContext : IdentityDbContext<EventsUser>
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
        { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<EventSubType> EventSubTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>()
                .Property(c => c.OrderStatus)
                .HasConversion<string>();

            modelBuilder.Entity<EventType>()
                .Property(c => c.Name)
                .HasConversion<string>();

            modelBuilder.Entity<EventSubType>()
                .Property(c => c.Name)
                .HasConversion<string>();

            modelBuilder.Entity<EventsUser>()
                .HasOne(a => a.Cart)
                .WithOne(b => b.User)
                .HasForeignKey<Cart>(a => a.UserName);

            modelBuilder.Entity<Event>()
                .HasOne(a => a.Type)
                .WithMany(b => b.Events)
                .HasForeignKey(u => u.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventSubType>()
                .HasOne(a => a.TypeParent)
                .WithMany(b => b.SubTypes)
                .HasForeignKey(u => u.TypeParentId);

            modelBuilder.Entity<Event>()
                .HasOne(a => a.Ticket)
                .WithOne(b => b.TicketEvent)
                .HasForeignKey<Event>(b => b.TicketId);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "1", Name = "User", NormalizedName = "User" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2", Name = "Administrator", NormalizedName = "Admin" });

            var andra = new Artist { Id = Guid.NewGuid(), Name = "Andra", Description = "Famous Romanian singer.", PhotoPath = "/img/andra.jpg" };
            var celine = new Artist { Id = Guid.NewGuid(), Name = "Celine Dion", Description = "Famous Canadian singer.", PhotoPath = "/img/works/3.jpg" };
            modelBuilder.Entity<Artist>().HasData(andra);
            modelBuilder.Entity<Artist>().HasData(celine);

            var concerts = new EventType { Id = Guid.NewGuid(), Name = TypeEnum.Concerts };
            var cultural = new EventType { Id = Guid.NewGuid(), Name = TypeEnum.Cultural };
            var family = new EventType { Id = Guid.NewGuid(), Name = TypeEnum.Family };
            var sports = new EventType { Id = Guid.NewGuid(), Name = TypeEnum.Sports };
            var others = new EventType { Id = Guid.NewGuid(), Name = TypeEnum.Others };
            modelBuilder.Entity<EventType>().HasData(concerts);
            modelBuilder.Entity<EventType>().HasData(cultural);
            modelBuilder.Entity<EventType>().HasData(family);
            modelBuilder.Entity<EventType>().HasData(sports);
            modelBuilder.Entity<EventType>().HasData(others);

            var craiova = new Location { Id = Guid.NewGuid(), City = "Craiova", Country = "Romania", Description = "Most popular for artists like: Crsti Minculescu, Tudor Gheorghe etc." };
            var buc = new Location { Id = Guid.NewGuid(), City = "Bucharest", Country = "Romania", Description = "Capital of Romania" };
            modelBuilder.Entity<Location>().HasData(craiova);
            modelBuilder.Entity<Location>().HasData(buc);

            base.OnModelCreating(modelBuilder);
        }
    }
}
