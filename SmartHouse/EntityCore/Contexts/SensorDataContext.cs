using Microsoft.EntityFrameworkCore;
using SmartHouse.EntityCore.Model;
using System.Configuration;

namespace SmartHouse.EntityCore.Context
{
    public class SensorDataContext : DbContext
    {
        public DbSet<DataModel> Data { get; set; }

        public SensorDataContext(DbContextOptions<SensorDataContext> options)
                    : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Database.db");
        }
    }
}
