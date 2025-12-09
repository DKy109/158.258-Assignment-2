using System.Collections.Generic;
using System.Data.Entity;

namespace WebAss2.Models
{
    public class BookDbContext : DbContext
    {
        public BookDbContext() : base("name=BookDbConnection")
        {
            // Initialize database with sample data
            Database.SetInitializer(new BookDbInitializer());
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Book>()
                .Property(b => b.Rating)
                .HasPrecision(2, 1);
        }
    }
}