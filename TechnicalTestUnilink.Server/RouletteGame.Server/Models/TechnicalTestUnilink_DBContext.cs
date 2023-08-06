using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RouletteGame.Server.Models
{
    public partial class TechnicalTestUnilink_DBContext : DbContext
    {
        public TechnicalTestUnilink_DBContext()
        {
        }

        public TechnicalTestUnilink_DBContext(DbContextOptions<TechnicalTestUnilink_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Player> Player { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["TechnicalTestUnilink_DBContext"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.PlayerName)
                    .HasName("PK__Player__F528443EFC912360");

                entity.Property(e => e.PlayerName)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PlayerBalance).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
