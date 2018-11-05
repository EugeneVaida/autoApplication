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
        [Required]        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime ManufacturerDate { get; set; }
        [Required]
        [Range(100, 1000000, ErrorMessage = "Power must be between 20 and 2000")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(20, 2000, ErrorMessage = "Power must be between 20 and 2000")]
        public int Power { get; set; }
        [Required]
        [Range(2, 100, ErrorMessage = "FuelConsumption must be between 2 and 100")]
        public decimal FuelConsumption { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Color { get; set; }    
        
        public int? ModelId { get; set; }
        public virtual Model Model { get; set; }

    }
}