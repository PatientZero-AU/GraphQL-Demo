using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ToughCuddles.Data.Models
{
    public partial class ToughCuddlesContext : DbContext
    {
        public virtual DbSet<Contestant> Contestants { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Umpire> Umpires { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ToughCuddles;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contestant>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Contestants)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contestant_Team");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AwayOdds).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.HomeOdds).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.AwayTeam)
                    .WithMany(p => p.MatchesAwayTeam)
                    .HasForeignKey(d => d.AwayTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matches_AwayTeams");

                entity.HasOne(d => d.HomeTeam)
                    .WithMany(p => p.MatchesHomeTeam)
                    .HasForeignKey(d => d.HomeTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matches_HomeTeams");

                entity.HasOne(d => d.Umpire)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.UmpireId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matches_Umpires");

                entity.HasOne(d => d.WinningTeam)
                    .WithMany(p => p.MatchesWinningTeam)
                    .HasForeignKey(d => d.WinningTeamId)
                    .HasConstraintName("FK_Matches_WinningTeams");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateSold).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Seat)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Matches");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.VenueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Venues");
            });

            modelBuilder.Entity<Umpire>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Venue>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
