using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHouse.EntityCore.Model
{
    public class DataModel
    {
        [Key]
        public int Id { get; set; }

        public string Temperature { get; set; }
        public string Wetness { get; set; }

        public DateTime Time { get; set; }
    }
}
