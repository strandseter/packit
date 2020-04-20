

using Packit.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace Packit.Model
{
    public abstract class BaseInformation : IDatabase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageStringName { get; set; }
        public int UserId { get; set; }

        public BaseInformation() { }

        public BaseInformation(string title, string description, string imageStringName)
        {
            Title = title;
            Description = description;
            ImageStringName = imageStringName;
        }

        public int GetUserId() => UserId;
        public abstract int GetId();
        public abstract void SetId(int value);
    }
}
