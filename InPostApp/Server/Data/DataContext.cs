using InPostApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InPostApp.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Locker> Lockers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipment>()
            .HasOne(x => x.Sender)
            .WithMany(x => x.SentShipments)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Shipment>()
            .HasOne(x => x.Receiver)
            .WithMany(x => x.ReceivedShipments)
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Shipment>()
            .HasOne(x => x.FromLocker)
            .WithMany(x => x.FromLockerDelivery)
            .HasForeignKey(x => x.FromLockerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Shipment>()
            .HasOne(x => x.ToLocker)
            .WithMany(x => x.ToLockerDelivery)
            .HasForeignKey(x => x.ToLockerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            //            modelBuilder.Entity<Shipment>()
            //.HasOne(x => x.ToLocker)
            //.WithMany(x => x.ToLockerDelivery)
            //.HasForeignKey(x => x.ToLockerId)
            //.OnDelete(DeleteBehavior.ClientSetNull);


        }
    }
}
