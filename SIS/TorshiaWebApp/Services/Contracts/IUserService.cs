using TorshiaWebApp.Models;

namespace TorshiaWebApp.Services.Contracts
{
    public interface IUserService
    {
        bool ExistsByUsernameAndPassword(string username, string password);

        void AddUserToDatabase(string username, string password, string email);

        User GetByUsername(string username);

        bool ExistsByUsername(string username);

        bool ExistsByEmail(string email);
    }
}
