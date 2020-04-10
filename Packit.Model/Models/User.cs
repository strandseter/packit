
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packit.Model
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        [NotMapped]
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
