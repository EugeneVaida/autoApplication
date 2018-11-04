using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace autoApp.Models
{
    public class Model
    {
        public Model() { }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public Model(string name, int id)
        {
            Name = name;
            ManufacturerId = id;
        }

        public virtual ICollection<Car> Cars { get; set; }
        [ForeignKey("Manufacturer")]
        public int? ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

    }
}