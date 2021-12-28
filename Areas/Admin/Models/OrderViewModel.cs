using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UncleApp.Areas.Admin.Models
{
    public class OrderViewModel
    {
        public string Customer_Name { set; get; }
        [BindNever]
        public string? address { set; get; }
        public DateTime Created { set; get; }
        public DateTime Updated { set; get; }
    }
}
