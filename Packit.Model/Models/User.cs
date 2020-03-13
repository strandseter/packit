using Packit.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class User : IOneTable
    {
        public int UserId { get; set; }
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Backpack> Backpacks { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }

        public User() 
        {
            Items = new List<Item>();
            Backpacks = new List<Backpack>();
            Trips = new List<Trip>();

            IdentityId = Guid.NewGuid().ToString();
        }

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

        public string GetIdentityId()
        {
            return IdentityId;
        }

        public int GetPrimaryId()
        {
            return UserId;
        }
    }
}
