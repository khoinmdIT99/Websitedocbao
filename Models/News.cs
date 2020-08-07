using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ltweb.Models
{
    public class News
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public string Title { get; set; }
        //[Required]
        public string Description { get; set; }
        public string SubDescription { get; set; }
        public string CoverImage { get; set; }
        [Required] public DateTime DateTime { get; set; } = DateTime.Now;
        public int? RegionId { get; set; }
        public Region Region { get; set; }
        //[Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public virtual ICollection<NewsImage> Images { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Region> Regions { get;set; }
    }

    public class NewsImage
    {
        public int Id { get; set; }
        [Required]
        public string Caption { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int NewsId { get; set; }
        public News News { get; set; }

    }
}