using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibraryMVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; }

        [Display(Name = "Icon Class (FontAwesome)")]
        public string IconClass { get; set; } = "fa-book";

        // Navigation property for books in this category
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}