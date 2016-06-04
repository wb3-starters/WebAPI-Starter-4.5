using System.Collections.Generic;

namespace WebAPI.Component.User
{
    public class User : Core.AuditableEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
