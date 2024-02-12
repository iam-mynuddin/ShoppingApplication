using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category name is required!")]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string CategoryName { get; set; }
        [DisplayName("Display Order")]
        [Range(0,100,ErrorMessage = "The field Display Order must be between 0-100!")]
        public int DisplayOrder {  get; set; }

    }
}
