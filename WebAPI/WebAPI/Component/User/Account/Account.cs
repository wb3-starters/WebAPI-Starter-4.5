using WebAPI.Core;

namespace WebAPI.Components.User.Account
{
    public class Account : AuditableEntity<int>
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool isPimaryAccount { get; set; }
        public AccountTypeEnum AccountType { get; set; }
        public string Password { get; set; }
        public string EncryptedPassword { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

}
