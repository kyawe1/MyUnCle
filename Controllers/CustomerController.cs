using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UncleApp.Context;

namespace UncleApp.Controllers
{
    [ApiController]
    [Authorize(Roles="Admin,Shopkeeper")]
    [Route("api/v1/shopkeeper/[controller]/[action]")]
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
            var customer = context.customers.SingleOrDefault(p => p.Fbname == Fbname);
            if(customer==null) return NotFound();
            return Ok(customer);
        }
    }
}
