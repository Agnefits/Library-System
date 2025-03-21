using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library_System.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }

        public ICollection<Book> Books { get; set; }
    }

}
