using Microsoft.AspNetCore.Mvc.ModelBinding;
using UncleApp.Models.Entity;
namespace UncleApp.Areas.Admin.Models
{
    public class CustomerViewModel
    {
        [BindNever]
        public int Id { set; get; }
        public string Fbname { get; set; }
        public string? realname { set; get; }
        public string PhoneNumber { set; get; }
        public string? address { set; get; }
        public static string getActiveAddres(ICollection<Address> address)
        {
            var q = (from v in address
                    where v.active == true
                    select v.Address_String).Single();
            return q;
                  
        }
    }
    
}
