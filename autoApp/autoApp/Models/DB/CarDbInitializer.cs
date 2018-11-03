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

            base.Seed(context);
        }
    }
}