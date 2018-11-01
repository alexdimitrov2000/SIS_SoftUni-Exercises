namespace TorshiaWebApp.Services
{
    using Contracts;
    using Data;
    using System;
    using System.Linq;
    using System.Net;
    using TorshiaWebApp.Models;

    public class UserService : IUserService
    {
        private readonly TorshiaDbContext context;
        private readonly IHashService hashService;

        public UserService(TorshiaDbContext context, IHashService hashService)
        {
            this.context = context;
            this.hashService = hashService;
        }

        public bool ExistsByUsernameAndPassword(string usernameOrEmail, string password)
        {
            string hashedPassword = this.hashService.Hash(password);

            bool userExists = this.context.Users
                .Any(u => (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.Password == hashedPassword);

            return userExists;
        }

        public void AddUserToDatabase(string username, string password, string email)
        {
            string hashedPassword = this.hashService.Hash(password);

            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Password = hashedPassword,
                Email = WebUtility.UrlDecode(email)
            };

            if (!this.context.Users.Any())
                user.Role = Models.Enums.Role.Admin;

            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public User GetByUsername(string username)
        {
            return this.context.Users.FirstOrDefault(u => u.Username == username);
        }

        public bool ExistsByUsername(string username)
        {
            return this.context.Users.Any(u => u.Username == username);
        }

        public bool ExistsByEmail(string email)
        {
            return this.context.Users.Any(u => u.Email == email);
        }
    }
}
