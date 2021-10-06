using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Core.DataModels.Users
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
