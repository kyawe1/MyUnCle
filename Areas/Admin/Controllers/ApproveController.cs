using Microsoft.AspNetCore.Mvc;
using UncleApp.Context;
using UncleApp.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace UncleApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
[ApiController]
[Route("api/[area]/[controller]/[action]")]
public class ApproveController : ControllerBase{
    private readonly DataContext _context;
    private readonly UserManager<IdentityUser> _role;

    public ApproveController(DataContext context,UserManager<IdentityUser> role){
        _context=context;
        _role=role;
    }
    [HttpGet]
    public IActionResult Index(bool pending=true){
        var q=_context.waitingLists.Include(p=> p.User).Where(p=> p.waiting==pending).Select(p=>new ApproveViewModel(){
            UserName=p.User.UserName,
            Email=p.User.Email,
            
        }).ToList();
        return Ok(q);
    }
    [HttpPost]
    [ActionName("create")]
    public async Task<IActionResult> AddToRole([FromBody]ApproveViewModel approve){
        var temp=_context.Users.FirstOrDefault(p=>p.UserName.Equals(approve.UserName));
        await _role.AddToRoleAsync(temp,"Shopkeeper");
        return Ok(new {message="all users",data=$"Added to role {temp.UserName}"});
    }
}