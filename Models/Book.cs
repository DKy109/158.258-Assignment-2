using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryMVC.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
        public string Author { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$",
            ErrorMessage = "Invalid ISBN format")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Publication year is required")]
        [Range(1000, 2024, ErrorMessage = "Publication year must be between 1000 and 2024")]
        public int PublicationYear { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000.00, ErrorMessage = "Price must be between $0.01 and $1000")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Display(Name = "Cover Image")]
        public string CoverImageUrl { get; set; }

        [Display(Name = "Available Copies")]
        [Range(0, 1000, ErrorMessage = "Copies must be between 0 and 1000")]
        public int AvailableCopies { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Navigation property for reviews
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}