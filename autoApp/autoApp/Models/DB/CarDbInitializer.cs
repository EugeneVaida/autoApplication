using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace autoApp.Models.DB
{
    public class CarDbInitializer : DropCreateDatabaseAlways<CarContext>
    {
        protected override void Seed(CarContext context)
        {
            /* Add models  */  
            Manufacturer honda = new Manufacturer("Honda");
            Manufacturer bmw = new Manufacturer("BMW");
            Manufacturer mersedes = new Manufacturer("Mersedes");
            Manufacturer audi = new Manufacturer("Audi");

            context.Manufacturers.AddRange(new List<Manufacturer> { honda, bmw, mersedes, audi });
            context.SaveChanges();

            Model audi1 = new Model("rs5", audi.Id);
            Model audi2 = new Model("a4", audi.Id);  
            Model bmw1 = new Model("535i", bmw.Id);
            Model bmw2 = new Model("320d", bmw.Id);
            Model honda1 = new Model("Civic", honda.Id);
            Model mersedes1 = new Model("S600", mersedes.Id);
            Model mersedes2 = new Model("C160", mersedes.Id);
            context.Models.AddRange(new List<Model> { audi1, audi2, bmw1, bmw2, honda1, mersedes1, mersedes2 });
            context.SaveChanges();


            context.SaveChanges();



            Car car1 = new Car
            {
                Color = "Black",
                Description = "Some information",
                Model = audi1,
                Price = 47600,
                ManufacturerDate = new DateTime(2015, 06, 15),
                Power = 510,
                FuelConsumption = 18,
                Image = ""
            };

            Car car2 = new Car
            {
                Color = "white",
                Description = "Some information",
                Model = bmw1,
                Price = 13200,
                ManufacturerDate = new DateTime(2013, 09, 11),
                Power = 320,
                FuelConsumption = 9.5M,
                Image = ""
            };

            context.Cars.AddRange(new List<Car> { car1, car2 });

            context.SaveChanges();
        }
    }
}