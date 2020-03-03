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
        public string HashedPassword { get; set; }
        public virtual ICollection<Item> Items { get;}
        public virtual ICollection<Backpack> Backpacks { get;}
        public virtual ICollection<Trip> Trips { get; }

        public User() { }

        public User(string firstName, string lastName, DateTime dateOfBirth, string email, string hashedPassword)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            HashedPassword = hashedPassword;
            Items = new List<Item>();
            Backpacks = new List<Backpack>();
            Trips = new List<Trip>();
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {UserId}";
        }
    }
}
