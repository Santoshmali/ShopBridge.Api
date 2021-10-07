using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Data.DbModels.Users
{
    public class UserRefreshToken : ModelBase
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [NotMapped]
        public bool IsExpired => DateTime.UtcNow >= Expires;
        [NotMapped]
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
