using Microsoft.AspNetCore.Mvc;
using UncleApp.Areas.Admin.Models;
using UncleApp.Context;
using UncleApp.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UncleApp.Areas.Admin
{
    
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private DataContext _dataContext { get; }
        public OrderController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var q = _dataContext.orders.Include(p=>p.Customer).ToList();
            List<OrderViewModel> b = new List<OrderViewModel>();
            foreach (var order in q)
            {
                var v=_dataContext.addresses.FirstOrDefault(p=>p.CustomerId==order.CustomerId && p.active==true);
                if (v != null)
                {
                    b.Add(new OrderViewModel()
                    {
                        address=v.Address_String,
                        Customer_Name=$"{order.Customer.Fbname} ({order.Customer.realname})",
                        Created=order.Created,
                    });  
                }
            }
            return View(b);
        }
        [HttpPost]
        public async Task<IActionResult> create(OrderViewModel order)
        {
            var user = _dataContext.customers.FirstOrDefault(p => order.Customer_Name.Contains(p.Fbname));
            if (user == null)
            {
                return BadRequest();
            }
            Order order1 = new Order()
            {
                Created=DateTime.UtcNow,
                CustomerId=user.Id,

            };
            _dataContext.Add(order1);
            await _dataContext.SaveChangesAsync();
            return Created("Created", new { message = "Your Customer's Order is Created" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(long id)
        {
            var order=_dataContext.orders.FirstOrDefault(p => p.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            _dataContext.orders.Remove(order);
            await _dataContext.SaveChangesAsync();
            return Ok(new { message = "This Order is successfully cencelled" });
        }
        
        
    }
}
