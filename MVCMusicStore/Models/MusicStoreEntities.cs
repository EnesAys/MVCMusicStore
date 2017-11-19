using MVCMusicStore.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCMusicStore.Models
{
    public class MusicStoreEntities : DbContext
    {
        public MusicStoreEntities()
            : base("MusicStoreCon")
        {

        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MusicStoreEntities>(new MyStrategy());

            //Genre
            modelBuilder.Entity<Genre>().Property(g => g.Name).HasMaxLength(50).IsRequired();
            //Artist
            modelBuilder.Entity<Artist>().Property(a => a.Name).HasMaxLength(150).IsRequired();
            //Album
            modelBuilder.Entity<Album>().Property(a => a.AlbumArtUrl).HasMaxLength(300);
            modelBuilder.Entity<Album>().Property(a => a.Price).HasColumnType("money");
            modelBuilder.Entity<Album>().Property(a => a.Title).IsRequired().HasMaxLength(150);
            //Order
            modelBuilder.Entity<Order>().Property(o => o.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Order>().Property(o => o.Phone).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Order>().Property(o => o.ShipAddress).HasMaxLength(300).IsRequired();
            //OrderDetail
            modelBuilder.Entity<OrderDetail>().Property(od => od.Price).HasColumnType("money");
            //UserDetail
            modelBuilder.Entity<UserDetail>().Property(ud => ud.Address).HasMaxLength(300);
            modelBuilder.Entity<UserDetail>().Property(ud => ud.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<UserDetail>().Property(ud => ud.FirstName).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<UserDetail>().Property(ud => ud.LastName).HasMaxLength(20);
            modelBuilder.Entity<UserDetail>().Property(ud => ud.Phone).HasMaxLength(20);
            modelBuilder.Entity<UserDetail>().Property(ud => ud.UserName).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<UserDetail>().Ignore(ud => ud.Password);
            modelBuilder.Entity<UserDetail>().Ignore(ud => ud.ConfirmPassword);
                
        }
    }
}