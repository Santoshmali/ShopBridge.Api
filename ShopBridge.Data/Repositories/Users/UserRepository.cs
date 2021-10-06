using Microsoft.EntityFrameworkCore;
using ShopBridge.Data.Context;
using ShopBridge.Data.DbModels.Users;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Data.Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ShopBridgeDbContext context) : base(context)
        {
        }

        public async Task<User> Authenticate(string username, string password)
        {
            return await this.Table.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }

        public async Task<User> GetUserByUserId(int id)
        {
            return await this.Table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUserRefreshToken(string refreshToken)
        {
            return  await this.Table.SingleOrDefaultAsync(x => x.RefreshTokens.Any(r => r.Token == refreshToken));
        }
    }
}
