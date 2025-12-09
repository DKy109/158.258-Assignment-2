using Microsoft.EntityFrameworkCore;

namespace BookLibraryMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fiction", Description = "Novels and short stories", IconClass = "fa-book-open" },
                new Category { Id = 2, Name = "Non-Fiction", Description = "Factual literature", IconClass = "fa-file-alt" },
                new Category { Id = 3, Name = "Science Fiction", Description = "Speculative fiction", IconClass = "fa-rocket" },
                new Category { Id = 4, Name = "Fantasy", Description = "Magical and mythical stories", IconClass = "fa-dragon" },
                new Category { Id = 5, Name = "Mystery", Description = "Crime and detective stories", IconClass = "fa-search" },
                new Category { Id = 6, Name = "Biography", Description = "Life stories", IconClass = "fa-user" },
                new Category { Id = 7, Name = "Technology", Description = "Computers and programming", IconClass = "fa-laptop-code" },
                new Category { Id = 8, Name = "Science", Description = "Scientific literature", IconClass = "fa-flask" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    ISBN = "978-0743273565",
                    PublicationYear = 1925,
                    Category = "Fiction",
                    Price = 12.99m,
                    Description = "A classic novel of the Jazz Age",
                    CoverImageUrl = "/images/books/great-gatsby.jpg",
                    AvailableCopies = 10,
                    DateAdded = DateTime.Now.AddDays(-30)
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    ISBN = "978-0061120084",
                    PublicationYear = 1960,
                    Category = "Fiction",
                    Price = 14.99m,
                    Description = "A novel about racial injustice in the American South",
                    CoverImageUrl = "/images/books/mockingbird.jpg",
                    AvailableCopies = 8,
                    DateAdded = DateTime.Now.AddDays(-25)
                },
                new Book
                {
                    Id = 3,
                    Title = "1984",
                    Author = "George Orwell",
                    ISBN = "978-0451524935",
                    PublicationYear = 1949,
                    Category = "Science Fiction",
                    Price = 9.99m,
                    Description = "A dystopian social science fiction novel",
                    CoverImageUrl = "/images/books/1984.jpg",
                    AvailableCopies = 12,
                    DateAdded = DateTime.Now.AddDays(-20)
                },
                new Book
                {
                    Id = 4,
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    ISBN = "978-1503290563",
                    PublicationYear = 1813,
                    Category = "Fiction",
                    Price = 8.99m,
                    Description = "A romantic novel of manners",
                    CoverImageUrl = "/images/books/pride.jpg",
                    AvailableCopies = 15,
                    DateAdded = DateTime.Now.AddDays(-15)
                },
                new Book
                {
                    Id = 5,
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    ISBN = "978-0547928227",
                    PublicationYear = 1937,
                    Category = "Fantasy",
                    Price = 16.99m,
                    Description = "A fantasy novel and children's book",
                    CoverImageUrl = "/images/books/hobbit.jpg",
                    AvailableCopies = 6,
                    DateAdded = DateTime.Now.AddDays(-10)
                },
                new Book
                {
                    Id = 6,
                    Title = "A Brief History of Time",
                    Author = "Stephen Hawking",
                    ISBN = "978-0553380163",
                    PublicationYear = 1988,
                    Category = "Science",
                    Price = 18.99m,
                    Description = "A popular-science book on cosmology",
                    CoverImageUrl = "/images/books/time.jpg",
                    AvailableCopies = 7,
                    DateAdded = DateTime.Now.AddDays(-5)
                },
                new Book
                {
                    Id = 7,
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    ISBN = "978-0132350884",
                    PublicationYear = 2008,
                    Category = "Technology",
                    Price = 35.99m,
                    Description = "A handbook of agile software craftsmanship",
                    CoverImageUrl = "/images/books/cleancode.jpg",
                    AvailableCopies = 9,
                    DateAdded = DateTime.Now.AddDays(-3)
                },
                new Book
                {
                    Id = 8,
                    Title = "The Da Vinci Code",
                    Author = "Dan Brown",
                    ISBN = "978-0307474278",
                    PublicationYear = 2003,
                    Category = "Mystery",
                    Price = 13.99m,
                    Description = "A mystery thriller novel",
                    CoverImageUrl = "/images/books/davinci.jpg",
                    AvailableCopies = 11,
                    DateAdded = DateTime.Now.AddDays(-1)
                }
            );

            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    BookId = 1,
                    ReviewerName = "John Smith",
                    Email = "john@example.com",
                    Rating = 5,
                    Comment = "An absolute masterpiece! The symbolism and themes are timeless.",
                    ReviewDate = DateTime.Now.AddDays(-10),
                    IsApproved = true
                },
                new Review
                {
                    Id = 2,
                    BookId = 1,
                    ReviewerName = "Alice Johnson",
                    Email = "alice@example.com",
                    Rating = 4,
                    Comment = "Beautiful writing, though the characters felt distant at times.",
                    ReviewDate = DateTime.Now.AddDays(-5),
                    IsApproved = true
                },
                new Review
                {
                    Id = 3,
                    BookId = 3,
                    ReviewerName = "Bob Wilson",
                    Email = "bob@example.com",
                    Rating = 5,
                    Comment = "Chillingly prophetic. A must-read for everyone.",
                    ReviewDate = DateTime.Now.AddDays(-7),
                    IsApproved = true
                }
            );
        }
    }
}