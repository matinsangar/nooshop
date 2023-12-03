namespace nooshop.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public double Credit { get; set; } = 9999;

    public string UserID { get; set; }
    public string Email { get; set; }
}