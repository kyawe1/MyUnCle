using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UncleApp.Context;
using UncleApp.Models.Entity;
using UncleApp.Models.ViewModel;

namespace UncleApp.Controllers;


[ApiController]
[Authorize(Roles ="Admin,Shopkeeper")]
[Route("api/v1/shopkeeper/[controller]/[action]/{id?}")]
public class OrderController : ControllerBase
{
    private DataContext _datacontext { get; }
    public OrderController(DataContext context)
    {
        _datacontext = context;
    }
    [HttpGet]
    [ActionName("Dumbling")]
    public IActionResult GetAllDumbling()
    {
        return Ok(new { message = "All dumbling types", data = _datacontext.dumblingTypes.Select(p => new { Name = p.Name, DumblingNumber = p.Id }).ToList() });
    }
    [HttpGet]
    public IActionResult Detail(int id)
    {
        var q = _datacontext.orders.FirstOrDefault(p => p.Id == id);
        if (q == null)
        {
            return NoContent();
        }
        var customer = _datacontext.customers.FirstOrDefault(p => p.Id == q.CustomerId);
        var address = _datacontext.addresses.Where(p => p.CustomerId == q.CustomerId).Select(p => new AddressViewModel()
        {
            Address = p.Address_String,
            active = p.active,
            Id = p.Id,
            CustomerId = p.CustomerId
        }).ToList();
        var items = _datacontext.orderItems.Where(p => p.OrderId == q.Id).Select(p => new OrderItemViewModel
        {
            dumblingId = p.dumblingid,
            items = p.numberofitems
        }).ToList();
        ShopkeeperUpdateViewModel createViewModel = new ShopkeeperUpdateViewModel()
        {
            Fbname = customer.Fbname,
            realname = customer.realname,
            PhoneNumber = customer.PhoneNumber,
            addresses = address,
            orderItems = items
        };
        return Ok(createViewModel);
    }
    [HttpPost]
    public async Task<IActionResult> Create(ShopkeeperCreateViewModel order)
    {
        ICollection<OrderItem> orders = new List<OrderItem>();

        Order? temp = null;
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        Customer c = _datacontext.customers.FirstOrDefault(p => p.Fbname == order.Fbname);
        foreach (var i in order.orderItems)
        {
            orders.Add(new OrderItem()
            {
                dumblingid = i.dumblingId,
                numberofitems = i.items
            });
        }
        if (c != null)
        {
            temp = new Order()
            {
                CustomerId = c.Id,
                Items = orders
            };
        }
        temp = new Order()
        {
            Customer = new Customer()
            {
                Fbname = order.Fbname,
                realname = order.realname,
                PhoneNumber = order.PhoneNumber,
                address = new List<Address>{
                    new Address(){
                        Address_String=order.addresses,
                        active=true
                    }
                }
            },
            Items = orders
        };
        _datacontext.Add(temp);
        await _datacontext.SaveChangesAsync();
        return Created("created", "this is created");
    }

    [HttpPut]
    public async Task<IActionResult> Update(long id, ShopkeeperUpdateViewModel order)
    {
        if (ModelState.IsValid)
        {
            IList<OrderItem> added = new List<OrderItem>();
            IList<OrderItem> deleted = new List<OrderItem>();
            Address? address = null;
            var q = _datacontext.orders.Include(p => p.Customer).ThenInclude(c => c.address).Include(p => p.Items).FirstOrDefault(p => p.Id == id);
            if (q != null)
            {
                q.Customer.Fbname = order.Fbname;
                q.Customer.realname = order.realname;
                q.Customer.PhoneNumber = order.PhoneNumber;
                for (int item = 0; item < q.Customer.address.Count; item++)
                {
                    q.Customer.address.ElementAt(item).active = order.addresses.ElementAt(item).active;
                }
                if (q.Customer.address.Count < order.addresses.Count)
                {
                    address = new Address()
                    {
                        Address_String = order.addresses.Last().Address,
                        active = true,
                        CustomerId = q.CustomerId
                    };
                }
                for (int item = 0; item < q.Items.Count; item++)
                {
                    int j = 0;
                    for (j = 0; j < order.orderItems.Count; j++)
                    {
                        if (q.Items.ElementAt(item).dumblingid == order.orderItems.ElementAt(j).dumblingId)
                        {
                            q.Items.ElementAt(item).numberofitems = order.orderItems.ElementAt(j).items;
                            order.orderItems.Remove(order.orderItems.ElementAt(j));
                            break;
                        }
                    }
                    if (j == order.orderItems.Count)
                    {
                        deleted.Add(q.Items.ElementAt(item));
                    }
                }

                if (deleted.Count != 0)
                {
                    _datacontext.RemoveRange(deleted);
                }
                if (order.orderItems.Count != 0)
                {
                    foreach (var a in order.orderItems)
                    {
                        _datacontext.Add(new OrderItem()
                        {
                            OrderId = q.Id,
                            numberofitems = a.items,
                            dumblingid = a.dumblingId
                        });
                    }
                }
                if (address != null)
                {
                    _datacontext.addresses.Add(address);
                }
                _datacontext.Update(q);
                await _datacontext.SaveChangesAsync();
                return Ok("Your object is Updated");
            }
        }

        return BadRequest();
    }
}