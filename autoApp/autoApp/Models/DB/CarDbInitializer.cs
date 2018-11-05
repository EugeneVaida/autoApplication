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
            Manufacturer mersedes = new Manufacturer("Mersedes");
            Manufacturer audi = new Manufacturer("Audi");

            context.Manufacturers.AddRange(new List<Manufacturer> { honda, mersedes, audi });
            context.SaveChanges();

            Model audi1 = new Model("rs5", audi.Id);
            Model audi2 = new Model("a4", audi.Id);  
            Model audi3 = new Model("a6", audi.Id);
            Model honda1 = new Model("Civic", honda.Id);
            Model honda2 = new Model("CRV", honda.Id);
            Model mersedes1 = new Model("S600", mersedes.Id);
            Model mersedes2 = new Model("C160", mersedes.Id);
            Model mersedes3 = new Model("E500", mersedes.Id);
            context.Models.AddRange(new List<Model> { audi1, audi2, honda1, mersedes1, mersedes2 });
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
                Image = "http://localhost:2780/files/img/audi_rs5.jpg"
            };

            Car car2 = new Car
            {
                Color = "Blue",
                Description = "Some information",
                Model = audi2,
                Price = 7500,
                ManufacturerDate = new DateTime(2001, 01, 11),
                Power = 120,
                FuelConsumption = 8.5M,
                Image = "http://localhost:2780/files/img/audi_a4.jpg"
            };

            Car car3 = new Car
            {
                Color = "Grey",
                Description = "Some information",
                Model = audi2,
                Price = 13200,
                ManufacturerDate = new DateTime(2013, 09, 11),
                Power = 180,
                FuelConsumption = 9.5M,
                Image = "http://localhost:2780/files/img/audi_a6.jpg"
            };

            Car car4 = new Car
            {
                Color = "Green",
                Description = "Some information",
                Model = audi3,
                Price = 9900,
                ManufacturerDate = new DateTime(2016, 05, 11),
                Power = 390,
                FuelConsumption = 9.5M,
                Image = "http://localhost:2780/files/img/audi_a6.jpg"
            };

            Car car5 = new Car
            {
                Color = "Yellow",
                Description = "Some information",
                Model = honda1,
                Price = 13200,
                ManufacturerDate = new DateTime(2017, 09, 11),
                Power = 360,
                FuelConsumption = 5.2M,
                Image = "http://localhost:2780/files/img/honda_civic.jpg"
            };

            Car car6 = new Car
            {
                Color = "Blue",
                Description = "Some information",
                Model = honda2,
                Price = 17500,
                ManufacturerDate = new DateTime(2016, 04, 27),
                Power = 170,
                FuelConsumption = 11M,
                Image = "http://localhost:2780/files/img/honda_crv.jpg"
            };

            Car car7 = new Car
            {
                Color = "White",
                Description = "Some information",
                Model = honda2,
                Price = 6200,
                ManufacturerDate = new DateTime(2011, 02, 17),
                Power = 190,
                FuelConsumption = 9.5M,
                Image = "http://localhost:2780/files/img/honda_crv.jpg"
            };

            Car car8 = new Car
            {
                Color = "Black",
                Description = "Some information",
                Model = mersedes1,
                Price = 79900,
                ManufacturerDate = new DateTime(2011, 09, 11),
                Power = 612,
                FuelConsumption = 29.5M,
                Image = "http://localhost:2780/files/img/mercedes_s600.jpg"
            };

            Car car9 = new Car
            {
                Color = "Grey",
                Description = "Some information",
                Model = mersedes2,
                Price = 23200,
                ManufacturerDate = new DateTime(2014, 09, 15),
                Power = 220,
                FuelConsumption = 10M,
                Image = "http://localhost:2780/files/img/mersedes_c180.jpg"
            };

            Car car10 = new Car
            {
                Color = "Black",
                Description = "Some information",
                Model = mersedes3,
                Price = 25000,
                ManufacturerDate = new DateTime(1995, 05, 01),
                Power = 340,
                FuelConsumption = 17M,
                Image = "http://localhost:2780/files/img/mersedes_e500.jpg"
            };

            Car car11 = new Car
            {
                Color = "White",
                Description = "Some information",
                Model = mersedes3,
                Price = 13200,
                ManufacturerDate = new DateTime(1998, 09, 11),
                Power = 345,
                FuelConsumption = 15M,
                Image = "http://localhost:2780/files/img/mersedes_e500.jpg"
            };

            Car car12 = new Car
            {
                Color = "Red",
                Description = "Some information",
                Model = honda1,
                Price = 18600,
                ManufacturerDate = new DateTime(2016, 06, 16),
                Power = 120,
                FuelConsumption = 5M,
                Image = "http://localhost:2780/files/img/honda_civic.jpg"
            };

            context.Cars.AddRange(new List<Car> { car1, car2, car3, car4, car5, car6, car7, car8, car9, car10, car11, car12 });

            context.SaveChanges();
        }
    }
}