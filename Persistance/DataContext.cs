using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Persistance
{

    public class DataContext : DbContext
    {
        public DbSet<WeatherForecast>? WeatherForecasts { get; set; }

        public DbSet<Post> posts { get; set; }

        public string dbPath { get; set; }

        public DataContext()
        {
            Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
            string path = Environment.GetFolderPath(folder);

            dbPath = Path.Join(path, "BlogBox.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={dbPath}");
        }
    }
}