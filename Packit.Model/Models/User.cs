using Packit.Model.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packit.Model
{
    public class User : Observable
    {
        private string firstName;
        private string lastName;
        private DateTimeOffset dateOfBirth;
        private string email;
        private string hashedPassword;

        public int UserId { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName 
        { 
            get => firstName;
            set => Set(ref firstName, value);
        }

        [Required]
        [StringLength(30)]
        public string LastName 
        { 
            get => lastName; 
            set => Set(ref lastName, value); 
        }

        [Required]
        public DateTimeOffset DateOfBirth 
        { 
            get => dateOfBirth; 
            set => Set(ref dateOfBirth, value); 
        }

        [Required]
        [StringLength(50)]
        public string Email 
        { 
            get => email; 
            set => Set(ref email, value); 
        }

        [Required]
        public string HashedPassword 
        { 
            get => hashedPassword; 
            set => Set(ref hashedPassword, value); 
        }

        [NotMapped]
        public string JwtToken { get; set; }
        public virtual ICollection<Item> Items { get; } = new List<Item>();
        public virtual ICollection<Backpack> Backpacks { get; } = new List<Backpack>();
        public virtual ICollection<Trip> Trips { get; } = new List<Trip>();

        public User()
        {
        }

        public override string ToString() => $"{FirstName} {LastName}, {UserId}";
    }
}
