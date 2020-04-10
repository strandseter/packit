using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Packit.DataAccess;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Packit.Database.Api.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings AppSettings;

        public AuthenticationService(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings?.Value;
        }

        public User Authenticate(string email, string hashedPassword)
        {
            using (var db = new PackitContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Email == email && u.HashedPassword == hashedPassword);

                if (user == null)
                    return null;

                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim("id", user.UserId.ToString())
                }),
                    Expires = DateTime.UtcNow.AddDays(300),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                user.JwtToken = handler.WriteToken(handler.CreateToken(descriptor));

                user.HashedPassword = null;

                return user;
            }
        }
    }
}
