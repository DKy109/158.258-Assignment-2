using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryMVC.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Reviewer name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Reviewer Name")]
        public string ReviewerName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Review text is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Review must be between 10 and 500 characters")]
        public string Comment { get; set; }

        [Display(Name = "Review Date")]
        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; } = DateTime.Now;

        [Display(Name = "Approved")]
        public bool IsApproved { get; set; } = false;

        // Foreign key for Book
        [Required]
        public int BookId { get; set; }

        // Navigation property
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}