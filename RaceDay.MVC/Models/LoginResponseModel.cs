namespace RaceDay.MVC.Models
{
    public class LoginResponseModel
    {
        public string Message { get; set; } = string.Empty;

        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}