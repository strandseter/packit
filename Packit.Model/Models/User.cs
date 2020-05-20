
using Packit.Model.Abstractions;
using Packit.Model.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packit.Model
{
    public class User : BaseModel
    {
        private string firstName;
        private string lastName;
        private DateTimeOffset dateOfBirth;
        private string email;
        private string hashedPassword;

        public int UserId { get; set; }

        public string FirstName 
        { 
            get => firstName;
            set => Set(ref firstName, value);
        }
        public string LastName 
        { 
            get => lastName; 
            set => Set(ref lastName, value); 
        }
        public DateTimeOffset DateOfBirth 
        { 
            get => dateOfBirth; 
            set => Set(ref dateOfBirth, value); 
        }
        public string Email 
        { 
            get => email; 
            set => Set(ref email, value); 
        }
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
