namespace RaceDay.MVC.Helpers
{
    public static class SessionExtensions
    {
        public static bool IsLoggedIn(HttpContext context)
        {
            return context.Session.GetInt32("UserId") != null;
        }

        public static string? GetRole(HttpContext context)
        {
            return context.Session.GetString("Role");
        }
    }
}