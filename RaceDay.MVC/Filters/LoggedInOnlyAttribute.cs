using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RaceDay.MVC.Filters
{
    public class LoggedInOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId =
                context.HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                context.Result =
                    new RedirectToActionResult(
                        "Login",
                        "Account",
                        null);
            }
        }
    }
}