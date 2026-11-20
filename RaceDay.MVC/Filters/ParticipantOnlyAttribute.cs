using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RaceDay.MVC.Filters
{
    public class ParticipantOnlyAttribute
        : ActionFilterAttribute
    {
        public override void OnActionExecuting(
            ActionExecutingContext context)
        {
            var role =
                context.HttpContext.Session.GetString("Role");

            if (role != "Participant")
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