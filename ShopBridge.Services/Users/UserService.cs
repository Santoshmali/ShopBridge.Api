using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopBridge.Core.DataModels;
using ShopBridge.Core.DataModels.Users;
using ShopBridge.Core.Extensions;
using ShopBridge.Data.DbModels.Users;
using ShopBridge.Data.Repositories.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;

        public UserService(IUserRepository userRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository.ThrowIfNull(nameof(userRepository));
            _mapper = mapper.ThrowIfNull(nameof(mapper));
            _appSettings = appSettings.ThrowIfNull(nameof(appSettings));
        }
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            model.ThrowIfNull(nameof(model));

            var user = await _userRepository.Authenticate(model.Username, model.Password);

            // return null if user not found
            if (user.IsNull())
            {
                return null;
            }

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = generateJwtToken(user);
            var refreshToken = generateRefreshToken(ipAddress);

            // save refresh token
            if(user.RefreshTokens.IsNullOrEmpty())
            {
                user.RefreshTokens = new List<UserRefreshToken>();
            }
            user.RefreshTokens.Add(refreshToken);

            user = await _userRepository.Update(user);

            return new AuthenticateResponse(_mapper.Map<UserModel>(user), jwtToken, refreshToken.Token);
        }

        public async Task<UserModel> GetById(int id)
        {
            var user = await _userRepository.GetUserByUserId(id);

            // return null if user not found
            if (user.IsNull())
            {
                return null;
            }

            return _mapper.Map<UserModel>(user);
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var user = await _userRepository.GetUserByUserRefreshToken(token);

            // return null if user not found
            if (user.IsNull())
            {
                return null;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);

            user = await _userRepository.Update(user);

            // generate new jwt
            var jwtToken = generateJwtToken(user);

            return new AuthenticateResponse(_mapper.Map<UserModel>(user), jwtToken, refreshToken.Token);
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await _userRepository.GetUserByUserRefreshToken(token);

            // return null if user not found
            if (user.IsNull())
            {
                return false;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            user = await _userRepository.Update(user);

            return true;
        }

        

        #region JWT

        private string generateJwtToken(Data.DbModels.Users.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private UserRefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new UserRefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        #endregion


    }
}
