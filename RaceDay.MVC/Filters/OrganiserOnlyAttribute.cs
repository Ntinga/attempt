using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RaceDay.MVC.Filters
{
    public class OrganiserOnlyAttribute
        : ActionFilterAttribute
    {
        public override void OnActionExecuting(
            ActionExecutingContext context)
        {
            var role =
                context.HttpContext.Session.GetString("Role");

            if (role != "Organiser")
            {
                context.Result =
                    new RedirectToActionResult(
                        "AccessDenied",
                        "Home",
                        null);
            }
        }
    }
}