using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UncleApp.Context;
using UncleApp.Models.ViewModel;
using UncleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace UncleApp.Controllers
{
    [ApiController]
    [Authorize(Roles="Admin,Shopkeeper")]
    [Route("api/v1/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private DataContext context;
        public CustomerController(DataContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index(string Fbname)
        {
            var customer = context.customers.Include(p=> p.address).Select(p=> new Models.ViewModel.CustomerViewModel
            {
                Fbname = p.Fbname,
                address= Models.ViewModel.CustomerViewModel.getActiveAddress(p.address),
                realname=p.realname,
                PhoneNumber=p.PhoneNumber,
                Id=p.Id
            }).SingleOrDefault(p => p.Fbname == Fbname);
            
            if(customer==null) return NotFound();
            return Ok(customer);
        }
    }
}
