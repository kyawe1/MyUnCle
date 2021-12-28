using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UncleApp.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public string Name { set; get; }
        public string? Description { set; get; }
        public double price { set; get; }
        public DateTime Created { set; get; }
        public DateTime? Updated { set; get; }
    }
    public class ProductCreateViewModel
    {
        
        public string Name { set; get; }
        public string Description { set; get; }
        public double price { set; get; }
    }
}
