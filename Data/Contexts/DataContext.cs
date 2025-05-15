using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<LocationEntity> Locations { get; set; }
    public DbSet<TicketInfoEntity> TicketInfos { get; set; }
    public DbSet<TicketPurchaseEntity> TicketPurchases { get; set; }
    public DbSet<StatusEntity> Statuses { get; set; }

    #region Grok Advice

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // One-to-one: TicketInfoEntity -> EventEntity
            modelBuilder.Entity<TicketInfoEntity>()
                .HasOne(t => t.Event)
                .WithOne(e => e.TicketInfo)
                .HasForeignKey<EventEntity>(e => e.TicketInfoId)
                .IsRequired();

            // One-to-many: TicketInfoEntity -> TicketPurchaseEntity
            modelBuilder.Entity<TicketPurchaseEntity>()
                .HasOne(tp => tp.TicketInfo)
                .WithMany(ti => ti.TicketPurchases)
                .HasForeignKey(tp => tp.TicketInfoId)
                .IsRequired();

            // Ensure unique indexes for Tag and Status
            modelBuilder.Entity<TagEntity>()
                .HasIndex(t => t.TagName)
                .IsUnique();

            modelBuilder.Entity<StatusEntity>()
                .HasIndex(s => s.StatusName)
                .IsUnique();
        }

    #endregion
}