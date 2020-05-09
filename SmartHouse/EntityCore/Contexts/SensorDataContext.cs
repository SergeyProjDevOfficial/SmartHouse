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
    }
}
