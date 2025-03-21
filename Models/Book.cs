using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library_System.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PageCount { get; set; }

        public int AuthorID { get; set; }
        public Author Author { get; set; }

        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; }
    }
}
