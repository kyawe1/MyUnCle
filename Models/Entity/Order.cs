using System.ComponentModel.DataAnnotations;

namespace UncleApp.Models.Entity
{
    public class Order
    {
        [Key]
        public int Id { set; get; }
        public int CustomerId { set; get; }
        public Customer Customer { set; get; }
        public DateTime Created { set; get; } = DateTime.UtcNow;
        public DateTime Updated { set; get; }
        public virtual ICollection<OrderItem>? Items { set; get; }
       
    }
}
