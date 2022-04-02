using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UncleApp.Models;
using UncleApp.Context;
using UncleApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using UncleApp.ActionFilter;


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
        [ServiceFilter(typeof(NotAuthenticatedOnly))]
        public async Task<IActionResult> login(LoginViewModel _login)
        {
            try
            {
                string token = await handler.GenerateJsonTokenAsync(_login);
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest();
                }
                return Ok(token);
            }catch(ArgumentNullException ex)
            {
                return NotFound();
            }

        }
        [HttpPost]
        [AllowAnonymous]
        [ServiceFilter(typeof(NotAuthenticatedOnly))]
        public async Task<IActionResult> Register(RegisterViewModel _register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = _register.UserName,
                        Email = _register.UserName
                    };
                    IdentityResult result = await _user.CreateAsync(user, _register.Password);
                    if (result.Succeeded)
                    {
                        return Ok("Your account is created");
                    }
                }
                return BadRequest();
            }
            catch(ArgumentNullException ex)
            {
                return NotFound();
            }

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
