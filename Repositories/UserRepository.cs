using nooshop.Models;
using System.Data.Entity;
using nooshop.Data;

namespace nooshop.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext _app)
    {
        _appDbContext = _app;
    }

    public bool VerifyUserLogin(User user)
    {
        try
        {
            var getUser = _appDbContext.Users.FirstOrDefault(u => u.Username == user.Username);
            if (getUser != null && user.Password == getUser.Password)
            {
                return true;
            }

            Console.WriteLine("Wrong information for User login...");

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public double getUserCredit(string savedName)
    {
        var user = _appDbContext.Users.FirstOrDefault(u => u.Username == savedName);
        if (user != null)
        {
            return user.Credit;
        }
        return 0;
    }

    public User? GetUserByName(string userName)
    {
        return _appDbContext.Users.FirstOrDefault(u => u.Username == userName);
    }
}