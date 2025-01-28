using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using mPathProject.Context;
using mPathProject.Handlers;
using mPathProject.Models;

namespace mPathProject.Services
{
    public class JwtService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public JwtService(AppDbContext dbContext, IConfiguration configuration) {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<LoginResponseModel?> Authenticate(LoginRequestModel request)
        {
            if (string.IsNullOrWhiteSpace(request.email) || string.IsNullOrWhiteSpace(request.password))
                return null;

            var userAccount = await _dbContext.Users.FirstOrDefaultAsync(x => x.email == request.email);
            if (userAccount is null || !PasswordHashHandler.VerifyPassword(request.password, userAccount.password))
                return null;
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtRegisteredClaimNames.Name, request.email)
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha512Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponseModel
            {
                AccessToken = accessToken,
                Email = request.email,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,

            };

        }

    }
}
