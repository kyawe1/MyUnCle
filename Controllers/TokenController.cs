using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UncleApp.Models;
using UncleApp.Context;
using UncleApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace UncleApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJsonHandler handler;
        private readonly UserManager<IdentityUser> _user;
        private readonly DataContext _context;
        public TokenController(UserManager<IdentityUser> user, IConfiguration config,DataContext context)
        {
            handler = new JsonTokenGenerator(config, user);
            _user=user;
            _context=context;

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> login(LoginViewModel _login)
        {
            string token = await handler.GenerateJsonTokenAsync(_login);
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }
            return Ok(token);

        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> AddWaitList()
        {
            var user=await _user.FindByNameAsync(User.Identity.Name);
            _context.waitingLists.Add(new Models.Entity.VerifyWaitingList(){
                UserId=user.Id,
            });
            _context.SaveChanges();
            return Ok();
        }
    }
}
