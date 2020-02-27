using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string HashedPasword { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {UserId}";
        }
    }
}
