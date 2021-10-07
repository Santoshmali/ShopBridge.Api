using System.Collections.Generic;

namespace ShopBridge.Data.DbModels.Users
{
    public class User : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public IList<UserRefreshToken> RefreshTokens { get; set; }
    }
}
