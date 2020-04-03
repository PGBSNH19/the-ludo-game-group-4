using LudoGameEngine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class DataContext : DbContext
    {
        public DbSet<Player> Player { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Piece> Piece { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.Json")
                .Build();
            var defaultConnection = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(defaultConnection);
        }
    }
}
