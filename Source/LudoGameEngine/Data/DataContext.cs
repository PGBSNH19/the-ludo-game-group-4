using LudoGameEngine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace LudoGameEngine
{
    public class DataContext : DbContext
    {
        public DbSet<Player> Player { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Piece> Piece { get; set; }
        public DbSet<PlayerSession>PlayerSession { get; set;}
        public DbSet<PlayerPiece> PlayerPiece { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.Json")
                .Build();
            var defaultConnection = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(defaultConnection);
        }

        protected override void  OnModelCreating(ModelBuilder Builder)
        {
            Builder.Entity<PlayerSession>().HasKey(c => new { c.SessionId, c.PlayerId });
            Builder.Entity<PlayerSession>()
                .HasOne<Player>(c => c.PlayerRef)
                .WithMany(s => s.PlayerSession)
                .HasForeignKey(x => x.PlayerId);

            Builder.Entity<PlayerSession>()
                .HasOne<Session>(x => x.SessionRef)
                .WithMany(c => c.PlayerPiece)
                .HasForeignKey(x => x.SessionId);

            Builder.Entity<PlayerPiece>().HasKey(c => new { c.PlayerId, c.PieceId });
            Builder.Entity<PlayerPiece>()
                .HasOne<Player>(c => c.PlayerRef)
                .WithMany(c => c.PlayerPiece)
                .HasForeignKey(x => x.PlayerId);

            Builder.Entity<PlayerPiece>()
                .HasOne<Piece>(c => c.PieceRef)
                .WithMany(c => c.PlayerPiece)
                .HasForeignKey(x => x.PieceId);
        }
    }
}
