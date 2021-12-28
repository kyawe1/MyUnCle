using System.ComponentModel.DataAnnotations;

namespace UncleApp.Models.ViewModel;

public class ShopkeeperCreateViewModel
{
    public string Fbname { set; get; }
    public string? realname { set; get; }
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { set; get; }
    public string addresses { set; get; }
    public ICollection<OrderItemViewModel> orderItems{set;get;}
}
public class ShopkeeperUpdateViewModel
{
    public string Fbname { set; get; }
    public string? realname { set; get; }
    public string PhoneNumber { set; get; }
    public ICollection<AddressViewModel> addresses { set; get; }
    public ICollection<OrderItemViewModel> orderItems{set;get;}
}
public class AddressViewModel
{
    public string Address { set; get; }
    public int Id { set; get; }
    public int CustomerId{set;get;}
    public bool active { set; get; } = false;
    
}
public class AddressCreateViewModel
{
    public string Address { set; get; }

}
public class OrderItemViewModel
{
    public int dumblingId { set; get; }
    
    public int items { set; get; }
}
