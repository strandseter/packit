using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public abstract class BaseInformation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageStringName { get; set; }
        public User User { get; set; }

        public BaseInformation() { }

        public BaseInformation(string title, string description, string imageStringName, User user)
        {
            Title = title;
            Description = description;
            ImageStringName = imageStringName;
            User = user;
        }
    }
}
