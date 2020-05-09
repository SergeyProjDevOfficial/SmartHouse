using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartHouse.EntityCore.Model;
using System.Configuration;

namespace SmartHouse.EntityCore.Context
{
    public class SensorDataContext : DbContext
    {
        public DbSet<DataModel> Data { get; set; }
        private IConfiguration Configuration { get; set; }

        public SensorDataContext(DbContextOptions<SensorDataContext> options, IConfiguration configuration)
                    : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration["DefaultConnection"]);
        }
    }
}
