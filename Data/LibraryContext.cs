using Microsoft.EntityFrameworkCore;
using Library_System.Models;

namespace Library_System.Data
{
    public class LibraryCtx : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory) // Ensures the correct path
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection"); // Use the correct key name
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Database connection string is missing.");
                }

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author).WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher).WithMany(p => p.Books)
                .HasForeignKey(p => p.PublisherID)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorID = 1, Name = "George Orwell", Nationality = "British", BirthDate = new DateTime(1903, 6, 25) },
                new Author { AuthorID = 2, Name = "J.K. Rowling", Nationality = "British", BirthDate = new DateTime(1965, 7, 31) }
            );

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { PublisherID = 1, Name = "Secker & Warburg", Country = "United Kingdom", Website = "www.seckerwarburg.com" },
                new Publisher { PublisherID = 2, Name = "Bloomsbury", Country = "United Kingdom", Website = "www.bloomsbury.com" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { BookID = 1, Title = "1984", ISBN = "978-0451524935", PublishedDate = new DateTime(1949, 6, 8), PageCount = 328, AuthorID = 1, PublisherID = 1 },
                new Book { BookID = 2, Title = "Harry Potter and the Philosopher's Stone", ISBN = "978-0747532699", PublishedDate = new DateTime(1997, 6, 26), PageCount = 223, AuthorID = 2, PublisherID = 2 }
            );
        }
    }

}
