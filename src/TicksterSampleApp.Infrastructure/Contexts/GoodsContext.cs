using Microsoft.EntityFrameworkCore;
using TicksterSampleApp.Domain.Enums;
using TicksterSampleApp.Domain.Models;

namespace TicksterSampleApp.Infrastructure.Contexts;

public partial class SampleAppContext : DbContext
{
    public DbSet<Goods> Goods { get; set; }

    public void OnModelCreatingGoods(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Goods>()
        //    .HasOne(g => g.Restaurant)
        //    .WithMany(r => r.Goods)
        //    .IsRequired(false);

        //modelBuilder.Entity<Goods>()
        //    .HasOne(g => g.Event)
        //    .WithMany(e => e.Goods)
        //    .IsRequired(false);

        modelBuilder.Entity<Goods>()
            .Property(g => g.TicksterGoodsId)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<Goods>()
            .Property(g => g.Name)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<Goods>()
            .Property(g => g.ReceiptText)
            .HasColumnType("text");

        modelBuilder.Entity<Goods>()
            .Property(g => g.ArticleNumber)
            .HasColumnType("varchar(50)");

        modelBuilder.Entity<Goods>()
            .Property(g => g.PriceIncVatAfterDiscount)
            .HasColumnType("decimal(10, 2)");

        modelBuilder.Entity<Goods>()
            .Property(g => g.VatPortion)
            .HasColumnType("decimal(10, 2)");

        modelBuilder.Entity<Goods>()
            .Property(g => g.VatPercent)
            .HasColumnType("decimal(10, 2)");

        modelBuilder.Entity<Goods>()
            .Property(g => g.Section)
            .HasColumnType("varchar(50)");

        modelBuilder.Entity<Goods>()
            .Property(g => g.PartOfSeasonTokenGoodsId)
            .HasColumnType("varchar(50)");

        modelBuilder.Entity<Goods>()
            .Property(g => g.Type)
            .HasColumnType("varchar(50)")
            .HasConversion(v => v.ToString(),
            v => (GoodsType)Enum.Parse(typeof(GoodsType), v));
    }
}
