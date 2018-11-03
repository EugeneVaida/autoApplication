using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace autoApp.Models.DB
{
    public class CarContext : DbContext
    {
        static CarContext()
        {
            Database.SetInitializer<CarContext>(new CarDbInitializer());
        }

        public CarContext() : base("CarDB") { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

    }
}