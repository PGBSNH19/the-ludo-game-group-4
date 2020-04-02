using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class DataContext : DbContext
    {
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
