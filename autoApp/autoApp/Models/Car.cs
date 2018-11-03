using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace autoApp.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public DateTime ManufacturerDate { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Power { get; set; }
        public decimal FuelConsumption { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }    
        
        public int? ModelId { get; set; }
        public Model Model { get; set; }
        
    }
}