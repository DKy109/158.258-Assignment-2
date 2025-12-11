using System.Collections.Generic;
using System.Data.Entity;

namespace WebAss2.Models
{
    public class BookDbInitializer : DropCreateDatabaseIfModelChanges<BookDbContext>
    {
        protected override void Seed(BookDbContext context)
        {
            var categories = new List<Category>
            {
                new Category { Name = "Fiction", Description = "Fictional books and novels", IconClass = "fa-book" },
                new Category { Name = "Science", Description = "Scientific books and journals", IconClass = "fa-flask" },
                new Category { Name = "Technology", Description = "Technology and programming books", IconClass = "fa-laptop" },
                new Category { Name = "Biography", Description = "Biographies and memoirs", IconClass = "fa-user" },
                new Category { Name = "History", Description = "Historical books", IconClass = "fa-landmark" }
            };

            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var books = new List<Book>
            {
                new Book {
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    CategoryId = categories.Find(c => c.Name == "Fiction").Id,
                    ISBN = "978-0743273565",
                    PublishedYear = 1925,
                    Price = 12.99m,
                    Description = "A classic novel of the Jazz Age.",
                    Rating = 4.5m,
                    PageCount = 180,
                    Publisher = "Scribner",
                    Language = "English"
                },
                new Book {
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    CategoryId = categories.Find(c => c.Name == "Technology").Id,
                    ISBN = "978-0132350884",
                    PublishedYear = 2008,
                    Price = 45.99m,
                    Description = "A handbook of agile software craftsmanship.",
                    Rating = 4.7m,
                    PageCount = 464,
                    Publisher = "Prentice Hall",
                    Language = "English"
                },
                new Book {
                    Title = "A Brief History of Time",
                    Author = "Stephen Hawking",
                    CategoryId = categories.Find(c => c.Name == "Science").Id,
                    ISBN = "978-0553380163",
                    PublishedYear = 1988,
                    Price = 18.99m,
                    Description = "From the Big Bang to Black Holes.",
                    Rating = 4.6m,
                    PageCount = 256,
                    Publisher = "Bantam Books",
                    Language = "English"
                },
                new Book {
                    Title = "Steve Jobs",
                    Author = "Walter Isaacson",
                    CategoryId = categories.Find(c => c.Name == "Biography").Id,
                    ISBN = "978-1451648539",
                    PublishedYear = 2011,
                    Price = 25.99m,
                    Description = "The exclusive biography of Apple's visionary.",
                    Rating = 4.8m,
                    PageCount = 656,
                    Publisher = "Simon & Schuster",
                    Language = "English"
                },
                new Book {
                    Title = "Sapiens: A Brief History of Humankind",
                    Author = "Yuval Noah Harari",
                    CategoryId = categories.Find(c => c.Name == "History").Id,
                    ISBN = "978-0062316097",
                    PublishedYear = 2015,
                    Price = 22.99m,
                    Description = "From Stone Age to modern technology.",
                    Rating = 4.7m,
                    PageCount = 464,
                    Publisher = "Harper",
                    Language = "English"
                },
                new Book {
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    CategoryId = categories.Find(c => c.Name == "Fiction").Id,
                    ISBN = "978-0446310789",
                    PublishedYear = 1960,
                    Price = 14.99m,
                    Description = "A novel about racial injustice.",
                    Rating = 4.8m,
                    PageCount = 324,
                    Publisher = "J. B. Lippincott & Co.",
                    Language = "English"
                },
                new Book {
                    Title = "The Pragmatic Programmer",
                    Author = "David Thomas & Andrew Hunt",
                    CategoryId = categories.Find(c => c.Name == "Technology").Id,
                    ISBN = "978-0201616224",
                    PublishedYear = 1999,
                    Price = 39.99m,
                    Description = "Your journey to mastery.",
                    Rating = 4.6m,
                    PageCount = 352,
                    Publisher = "Addison-Wesley",
                    Language = "English"
                },
                new Book {
                    Title = "1984",
                    Author = "George Orwell",
                    CategoryId = categories.Find(c => c.Name == "Fiction").Id,
                    ISBN = "978-0451524935",
                    PublishedYear = 1949,
                    Price = 16.99m,
                    Description = "A dystopian social science fiction novel.",
                    Rating = 4.9m,
                    PageCount = 328,
                    Publisher = "Secker & Warburg",
                    Language = "English"
                },
                new Book {
                    Title = "The Selfish Gene",
                    Author = "Richard Dawkins",
                    CategoryId = categories.Find(c => c.Name == "Science").Id,
                    ISBN = "978-0198788607",
                    PublishedYear = 1976,
                    Price = 21.99m,
                    Description = "A revolutionary work in evolutionary biology.",
                    Rating = 4.5m,
                    PageCount = 368,
                    Publisher = "Oxford University Press",
                    Language = "English"
                },
                new Book {
                    Title = "Leonardo da Vinci",
                    Author = "Walter Isaacson",
                    CategoryId = categories.Find(c => c.Name == "Biography").Id,
                    ISBN = "978-1501139154",
                    PublishedYear = 2017,
                    Price = 32.99m,
                    Description = "A biography of the Italian polymath.",
                    Rating = 4.7m,
                    PageCount = 624,
                    Publisher = "Simon & Schuster",
                    Language = "English"
                }
            };

            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}