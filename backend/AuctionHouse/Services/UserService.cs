using AuctionHouse.Models;
using AuctionHouse.Repositories;
using System.Security.Claims;

namespace AuctionHouse.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IHashService hashService, ITokenService tokenService, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
        }

        public async Task<User> GetUserWithId(string token)
        {
            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(token);
            int id = Convert.ToInt32(principal.FindFirstValue("Id"));
            User user = await _userRepository.GetUserWithId(id);
            if (user == null)
                throw new Exception("No user exist");
            user.Password = "";
            return user;
        }

        public async Task<TokenApi> Login(User user)
        {
            User u = await _userRepository.GetUserWithNameAndEmail(user);
            if (u == null)
                throw new Exception("Wrong email or username. Please try again or register.");
            bool valid = _hashService.VerifyEncodedString(u.Password, user.Password);
            if (valid)
            {
                string accessToken = _tokenService.GenerateAccessToken(u);
                string refreshToken = _tokenService.GenerateRefreshToken();

                TokenApi tokenApi = new TokenApi
                {
                    AccessToken = accessToken,
                    RecoveryToken = refreshToken
                };

                RefreshToken refresh = new RefreshToken
                {
                    RecoveryToken = refreshToken,
                    UserId = u.Id,
                    ExpiredAt = DateTime.UtcNow.AddDays(7),
                };

                await _tokenRepository.InsertRecoveryToken(refresh);

                return tokenApi;
            }
            throw new Exception("Wrong credentials");
        }

        public async Task<RegisterationResponse> Register(User user)
        {
            user.Password = _hashService.EncodeToBase64(user.Password);
            RegisterationResponse registerationResponse = await _userRepository.Register(user);
            return registerationResponse;
        }
    }
}
