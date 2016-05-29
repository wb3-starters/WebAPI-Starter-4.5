using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Components.User.Account;

namespace WebAPI.Components.User
{
    public class User : Core.AuditableEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Account.Account> Accounts { get; set; }
    }
}
