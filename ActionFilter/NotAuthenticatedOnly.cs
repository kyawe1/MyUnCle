using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;


namespace UncleApp.ActionFilter;

public class NotAuthenticatedOnly : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if(context.HttpContext.User.Identity.IsAuthenticated)
        {
            if(context.HttpContext.Request.ContentType=="application/json")
            {
                context.Result=new JsonResult(new {message="You are already logged in"});
            }
            context.Result=new RedirectToActionResult("Index","Home",new{});
        }
    }
}