using KOULIBALY.Models.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Poule> Poules { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            modelBuilder.Entity<Game>()
                        .HasOne(a => a.HomeTeam)
                        .WithMany(b => b.HomeGames)
                        .HasForeignKey(b => b.HomeTeamId);

            modelBuilder.Entity<Game>()
                        .HasOne(a => a.AwayTeam)
                        .WithMany(b => b.AwayGames)
                        .HasForeignKey(b => b.AwayTeamId);

            modelBuilder.Entity<Game>()
                        .HasOne(a => a.HomeTeam)
                        .WithMany(b => b.HomeGames)
                        .HasForeignKey(u => u.HomeTeamId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                        .HasOne(a => a.AwayTeam)
                        .WithMany(b => b.AwayGames)
                        .HasForeignKey(u => u.AwayTeamId)
                        .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
