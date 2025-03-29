namespace WeatherApp;

    using Microsoft.EntityFrameworkCore;

    internal class WeatherDbContext : DbContext
    {
        public DbSet<WeatherRecord> WeatherRecords { get; set; } 

        public WeatherDbContext() 
        {
            Database.EnsureCreated(); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=weather_data.db"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
