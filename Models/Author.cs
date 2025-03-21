using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library_System.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
