using ShopBridge.Data.DbModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Data.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Authenticate(string username, string password);
        Task<User> GetUserByUserId(int id);
        Task<User> GetUserByUserRefreshToken(string refreshToken);
    }
}
