using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ltweb.Helper;

namespace ltweb.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [NotMapped]
        public string StrippedName
        {
            get => Name.StripVn();
        }

        public Category() { }

        public Category(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}