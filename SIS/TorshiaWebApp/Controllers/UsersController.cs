namespace TorshiaWebApp.Controllers
{
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Method;
    using SIS.Framework.Controllers;
    using SIS.Framework.Security;
    using SIS.HTTP.Exceptions;
    using System.Collections.Generic;
    using TorshiaWebApp.Services.Contracts;
    using TorshiaWebApp.ViewModels;

    public class UsersController : Controller
    {
        private const string IndexView = "/";
        private const string PasswordsErrorMessage = "Passwords do not match.";
        private const string UserNotFoundErrorMessage = "No users found with the given combination of username/email and password";

        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            string usernameOrEmail = model.Username;
            string password = model.Password;

            var userExists = this.userService.ExistsByUsernameAndPassword(usernameOrEmail, password);

            if (!userExists)
            {
                throw new BadRequestException(UserNotFoundErrorMessage);
            }

            var user = this.userService.GetByUsername(usernameOrEmail);

            this.SignIn(new IdentityUser { Username = usernameOrEmail, Password = password, Roles = new List<string> { user.Role.ToString() } });
            return this.RedirectToAction(IndexView);
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            string username = model.Username;
            string password = model.Password;
            string confirmPassword = model.ConfirmPassword;
            string email = model.Email;

            if (password != confirmPassword)
            {
                throw new BadRequestException(PasswordsErrorMessage);
            }

            try
            {
                this.userService.AddUserToDatabase(username, password, email);
            }
            catch (System.Exception e)
            {
                throw new BadRequestException(e.Message);
            }

            var user = this.userService.GetByUsername(username);

            this.SignIn(new IdentityUser { Username = username, Password = password, Roles = new List<string> { user.Role.ToString() } });
            return this.RedirectToAction(IndexView);
        }

        public IActionResult Logout()
        {
            this.SignOut();

            return this.RedirectToAction(IndexView);
        }
    }
}
