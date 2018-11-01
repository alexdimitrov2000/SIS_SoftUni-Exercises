namespace TorshiaWebApp.Models
{
    using Enums;

    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Reports = new List<Report>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
