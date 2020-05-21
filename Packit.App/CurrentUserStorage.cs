using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App
{
    public static class CurrentUserStorage
    {
        public static User User { get; set; } = new User() { JwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQiLCJpZCI6IjQiLCJuYmYiOjE1OTAwNTQwNjUsImV4cCI6MTYxNTk3NDA2NSwiaWF0IjoxNTkwMDU0MDY1fQ.CHQXKvYU_krKinTITJNg1eAIfGZ3G20rQW46xBsWDZA" };
    }
}
