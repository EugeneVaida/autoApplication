﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace autoApp.Models
{
    public class Manufacturer
    {
        public Manufacturer() { }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public Manufacturer(string name)
        {
            Name = name;
        }

        public virtual ICollection<Model> Models { get; set; }
    }
}