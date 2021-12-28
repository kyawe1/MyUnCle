using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UncleApp.Models.Entity
{
    public class OrderItem
    {
        [Key]
        public int Id { set; get; }
        [ForeignKey("Type")]
        public int dumblingid { set; get; }
        public DumblingType Type { set; get; }
        public int OrderId { set; get; }
        public Order Order { set; get; }
        public int numberofitems { set; get; }
    }
}
