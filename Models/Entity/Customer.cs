using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UncleApp.Models.Entity
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Fbname { get; set; }    
        public string? realname { set; get; }
        public string PhoneNumber { set; get; }
        
        public ICollection<Address> address { set; get; }
    }
}
