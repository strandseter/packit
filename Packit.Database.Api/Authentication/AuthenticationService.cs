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
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                    Expires = DateTime.UtcNow.AddDays(7), //TODO: Change days??
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                user.JwtToken = handler.WriteToken(handler.CreateToken(descriptor));

                db.SaveChanges();

                user.HashedPassword = null;

                return user;
            }
        }

        //TODO: Fix to async
        public IEnumerable<User> GetAll()
        {
            using (var db = new PackitContext())
            {
                var users = db.Users;

                foreach(User user in users)
                    user.HashedPassword = null;

                return users;
            }
        }
    }
}
