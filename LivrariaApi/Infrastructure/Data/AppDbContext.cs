using LivrariaApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais do modelo podem ser feitas aqui
            modelBuilder.Entity<Book>().HasKey(b => b.Id);

            modelBuilder.Entity<Book>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(b => b.Author)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(b => b.Genre)
                .HasMaxLength(50);

            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Book>()
                .Property(b => b.StockQuantity)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}