using SmartHouse.EntityCore.Context;
using SmartHouse.EntityCore.Model;
using System.Linq;

namespace SmartHouse.EntityCore.Helpers
{
    public class DbHelper
    {
        SensorDataContext context { get; set; }
        public DbHelper(SensorDataContext context)
        {
            this.context = context;
        }

        public void Add(DataModel data)
        {
            context.Add(data);

            //if (prewData == null)
            //{
            //    return;
            //}

            //if (prewData.Equals(data))
            //{
            //    return;
            //}
        }
    }
}
