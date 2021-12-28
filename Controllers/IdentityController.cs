using Microsoft.AspNetCore.Mvc;
using UncleApp.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using UncleApp.ActionFilter;

namespace UncleApp.Controllers
{
    public class IdentityController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        public IdentityController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        [ServiceFilter(typeof(NotAuthenticatedOnly))]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        [ServiceFilter(typeof(NotAuthenticatedOnly))]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(NotAuthenticatedOnly))]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result=await _signInManager.PasswordSignInAsync(model.UserName,model.Password,model.RememberMe,false);
            if(result.Succeeded){
                return RedirectToAction("Index","Home");
            }else if(result.IsNotAllowed){
                return StatusCode(500,new{message="Your account is not activated for signin"});
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(NotAuthenticatedOnly))]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid){
                return View(model);
            }
            IdentityUser u=new IdentityUser(){
                UserName=model.UserName,
                Email=model.UserName,
            };
            var result=await _userManager.CreateAsync(u,model.Password);
            if(result.Succeeded){
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        
    }
}
