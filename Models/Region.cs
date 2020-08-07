using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ltweb.Models
{
    public class Region//Miền Bắc Trung Nam
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Region() { }

        public Region(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}