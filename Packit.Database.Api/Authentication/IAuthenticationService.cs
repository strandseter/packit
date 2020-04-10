using Microsoft.EntityFrameworkCore;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Authentication
{
    public interface IAuthenticationService
    {
        User Authenticate(string email, string hashedPassword);
    }
}
