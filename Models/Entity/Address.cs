using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace UncleApp.Models.Entity
{
    public class Address
    {
        [Key]
        public int Id { set; get; }
        [ForeignKey("customer")]
        public int CustomerId { set; get; }
        public Customer customer { set; get; }
        public string Address_String { set; get; }
        public bool active { set; get; } = true;
    }
}
