using SmartHouse.EntityCore.Context;
using SmartHouse.EntityCore.Model;
using System.Linq;

namespace SmartHouse.EntityCore.Helpers
{
    public class DbAddingHelper
    {
        SensorDataContext context { get; set; }
        public DbAddingHelper(SensorDataContext context)
        {
            this.context = context;
        }

        public void AddToDb(DataModel data)
        {
            DataModel prewData = context.Data?.Last();

            if (prewData == null)
            {
                return;
            }

            if (prewData.Equals(data))
            {
                return;
            }
        }
    }
}
