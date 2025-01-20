using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Enums;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Goods> Goods { get; set; }

    public void OnModelCreatingGoods(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Goods>()
            .HasOne(g => g.Restaurant)
            .WithMany(r => r.Goods)
            .HasForeignKey(g => g.RestaurantId)
            .HasPrincipalKey(r => r.Id);

        modelBuilder.Entity<Goods>()
            .HasOne(g => g.Event)
            .WithMany(e => e.Goods)
            .HasForeignKey(g => g.EventId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Goods>()
            .Property(g => g.GoodsId)
            .HasMaxLength(255);

        modelBuilder.Entity<Goods>()
            .Property(g => g.Name)
            .HasMaxLength(255);

        modelBuilder.Entity<Goods>()
            .Property(g => g.ArticleNumber)
            .HasMaxLength(50);

        modelBuilder.Entity<Goods>()
            .Property(g => g.PriceIncVatAfterDiscount)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Goods>()
            .Property(g => g.VatPortion)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Goods>()
            .Property(g => g.VatPercent)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Goods>()
            .Property(g => g.Section)
            .HasMaxLength(50);

        modelBuilder.Entity<Goods>()
            .Property(g => g.PartOfSeasonTokenGoodsId)
            .HasMaxLength(50);

        modelBuilder.Entity<Goods>()
            .Property(g => g.Type)
            .HasConversion(v => v.ToString(),
            v => (GoodsType)Enum.Parse(typeof(GoodsType), v));
    }
}
