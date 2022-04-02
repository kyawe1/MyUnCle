using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UncleApp.Context;
using UncleApp.Areas.Admin.Models;
using UncleApp.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UncleApp.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private DataContext _dataContext { get; }
        public CustomerController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<CustomerViewModel>? customers;
            try
            {
                customers = _dataContext.customers.Include(p => p.address).Select(p => new CustomerViewModel()
                {
                    Id=p.Id,
                    Fbname = p.Fbname,
                    realname = p.realname,
                    PhoneNumber = p.PhoneNumber,
                    address = CustomerViewModel.getActiveAddress(p.address)
                }).ToList();
            }
            catch(Exception ex)
            {
                return NoContent();
            }
            return Ok(JsonSerializer.Serialize(customers));
        }
        [HttpGet("{id}")]
        public IActionResult Detail(long id)
        {
            var d=_dataContext.customers.Include(p=>p.address).FirstOrDefault(p => p.Id == id);
            if (d != null)
            {
                return Ok(new { data = d });
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> create([Bind(new String[] {
            "Fbname",
            "PhoneNumber",
            "realname",
            "address"
})]CustomerViewModel createViewModel)
        {
            if (!TryValidateModel(createViewModel))
            {
                return BadRequest(createViewModel);
            }
            try
            {
                Customer customer = new Customer()
                {
                    Fbname = createViewModel.Fbname,
                    PhoneNumber = createViewModel.PhoneNumber,
                    realname = createViewModel.realname,
                    address = new List<Address>
                {
                    new Address()
                    {
                        Address_String=createViewModel.address
                    }
                }
                };
                _dataContext.Add(customer);
                await _dataContext.SaveChangesAsync();
            }catch(Exception e)
            {
                return StatusCode(500, e.ToString());
            }
            return Created("create",new {message="Your Customer is Created"});
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(long id)
        {
            var q=_dataContext.customers.FirstOrDefault(p=>p.Id==id);
            if (q == null)
            {
                return NotFound();
            }
            _dataContext.Remove(q);
            await _dataContext.SaveChangesAsync();
            return Ok(new { message="Your Customer is Deleted" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id,[Bind(new String[] {
            "Fbname",
            "realname",
            "PhoneNumber",
            "address"
})] CustomerViewModel customer)
        {
            Address? aa=null;
            var customer1=_dataContext.customers.Include(p=>p.address).FirstOrDefault(p => p.Id == id);
            var a=_dataContext.addresses.FirstOrDefault(p => p.Address_String == customer.address.Trim());
            if (customer1 == null)
            {
                return NotFound();
            }
            Address address = _dataContext.addresses.FirstOrDefault(p => p.CustomerId == customer1.Id && p.active == true);
            if (a != null)
            {
                if (a.Id != address.Id)
                {
                    foreach(var i in customer1.address)
                    {
                        if (i.Id != a.Id)
                        {
                            i.active = false;
                        }
                        else
                        {
                            i.active = true;
                        }
                    }
                    
                    
                }
            }
            else
            {
                foreach (var i in customer1.address)
                {
                    i.active = false;
                }
                aa = new Address()
                {
                    CustomerId=customer1.Id,
                    Address_String = customer.address,
                    active=true
                };
                _dataContext.Add(aa);
            }
            customer1.Fbname = customer.Fbname;
            customer1.realname = customer.realname;
            customer1.PhoneNumber = customer.PhoneNumber;
            
            _dataContext.Update(customer1);
            await _dataContext.SaveChangesAsync();
            return Ok(new { message = "Your Customer is Updated" });
        }
        //[HttpPatch]
        //public IActionResult UpdatePatch()
        //{
        //    return Ok(new { message = "Your few column of customer is updated" });
        //}
    }
}
