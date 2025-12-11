using System;
using System.ComponentModel.DataAnnotations;

namespace WebAss2.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 200 characters")]
        [Display(Name = "Book Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // Navigation property
        public virtual Category Category { get; set; }

        [Display(Name = "ISBN")]
        [RegularExpression(@"^(?:\d{3}-)?\d{10}$", ErrorMessage = "Invalid ISBN format")]
        public string ISBN { get; set; }

        [Range(1900, 2024, ErrorMessage = "Published year must be between 1900 and 2024")]
        [Display(Name = "Published Year")]
        public int PublishedYear { get; set; }

        [Range(0.01, 1000.00, ErrorMessage = "Price must be between 0.01 and 1000.00")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Display(Name = "In Stock")]
        public bool InStock { get; set; } = true;

        [Display(Name = "Rating")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public decimal? Rating { get; set; }

        [Display(Name = "Cover Image")]
        public string CoverImage { get; set; }

        [Display(Name = "Page Count")]
        [Range(1, 5000, ErrorMessage = "Page count must be between 1 and 5000")]
        public int? PageCount { get; set; }

        [Display(Name = "Publisher")]
        public string Publisher { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; } = "English";
    }
}