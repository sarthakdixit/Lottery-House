using AuctionHouse.Models;
using AuctionHouse.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuctionHouse.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(IOptions<AppSettings> appSettings, IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Username", user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public async Task<TokenApi> RefreshToken(TokenApi tokenApi)
        {
            string accessToken = tokenApi.AccessToken;
            string recoveryToken = tokenApi.RecoveryToken;

            ClaimsPrincipal principal = GetPrincipalFromExpiredToken(accessToken);
            string username = principal.FindFirstValue("Username");
            string email = principal.FindFirstValue("Email");

            User u = new User
            {
                Username = username,
                Email = email
            };

            User user = await _userRepository.GetUserWithNameAndEmail(u);
            if (user is null)
                throw new Exception("Invalid client request");

            RefreshToken refreshToken = await _tokenRepository.GetTokenUsingUserIdAndRecoveryToken(user.Id, recoveryToken);
            if (refreshToken == null)
                throw new Exception("Wrong token");
            else if (refreshToken.ExpiredAt <= DateTime.UtcNow)
                throw new Exception("Recovery token expired. Please login again");

            string newAccessToken = GenerateAccessToken(user);
            string newRecoveryToken = GenerateRefreshToken();

            RefreshToken refresh = new RefreshToken
            {
                RecoveryToken = newRecoveryToken,
                UserId = user.Id,
                ExpiredAt = DateTime.UtcNow.AddDays(7),
            };

            await _tokenRepository.InsertRecoveryToken(refresh);

            return new TokenApi
            {
                AccessToken = newAccessToken,
                RecoveryToken = newRecoveryToken
            };

        }

        public void RevokeToken(string token)
        {
            ClaimsPrincipal principal = GetPrincipalFromExpiredToken(token);
            int id = Convert.ToInt32(principal.FindFirstValue("Id"));
            _tokenRepository.RevokeToken(id);
        }

        public async Task<TokenApi> ValidateToken(TokenApi tokenApi)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(tokenApi.AccessToken);

            if (jwtSecurityToken == null)
                throw new Exception("Invalid token details");

            TokenApi api = new TokenApi();
            if (jwtSecurityToken.ValidTo <= DateTime.UtcNow)
            {
                api = await RefreshToken(tokenApi);
            }

            return api;
        }
    }
}
