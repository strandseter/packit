
using System;
using System.Collections.Generic;

namespace Packit.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string JwtToken { get; set; }
        public virtual ICollection<Item> Items { get; } = new List<Item>();
        public virtual ICollection<Backpack> Backpacks { get; } = new List<Backpack>();
        public virtual ICollection<Trip> Trips { get; } = new List<Trip>();

        public User() 
        {
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {UserId}";
        }
    }
}
