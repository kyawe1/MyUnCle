using System.ComponentModel.DataAnnotations;

namespace UncleApp.Models.Entity
{
    public class DumblingType
    {
        [Key]
        public int Id { set; get; }
        [StringLength(300)]
        public string Name { set; get; }
        [StringLength(500)]
        public string Description { set; get; }
        public double Price { set; get; }
        public DateTime Created { set; get; } = DateTime.UtcNow;
        public DateTime? Updated { set; get; } 
        public virtual IList<OrderItem> items { set; get; }
    }
}
