namespace nooshop.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public double? Credit { get; set; } = 0;

    public int UserID { get; set; }
    public string Email { get; set; }
}