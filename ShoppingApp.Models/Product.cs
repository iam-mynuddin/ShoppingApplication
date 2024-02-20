using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        [Display(Name ="List Price")]
        [Required]
        [Range(1,1000)]
        public double ListPrice { get; set; }
        [Display(Name = "Price for 1-50")]
        [Required]
        [Range(1, 1000)]
        public double Price { get; set; }
        [Display(Name = "Price for 50-100")]
        [Required]
        [Range(1, 1000)]
        public double Price50 { get; set; }
        [Display(Name = "Price for 100+")]
        [Required]
        [Range(1, 1000)]
        public double Price100 { get; set; }
        [Display(Name ="Category")]
        public int CategoryId_FK {  get; set; }
        [ForeignKey("CategoryId_FK")]
        public Categories Categories { get; set; }
        public string ImgUrl { get; set; }
    }
}
