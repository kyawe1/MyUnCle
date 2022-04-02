using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UncleApp.Areas.Admin.Controllers
{
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
