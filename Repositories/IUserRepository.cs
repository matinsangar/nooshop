using nooshop.Models;

namespace nooshop.Repositories;

public interface IUserRepository
{
    User? GetUserByName(string userName);
    bool VerifyUserLogin(User user);
    double getUserCredit(string savedName);
}