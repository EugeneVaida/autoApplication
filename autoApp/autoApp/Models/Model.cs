using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace autoApp.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public Model(string name)
        {
            Name = Name;
        }

        public ICollection<Car> Cars { get; set; }
    }
}