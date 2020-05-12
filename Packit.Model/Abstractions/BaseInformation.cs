using Packit.Model.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Packit.Model
{
    public abstract class BaseInformation : IDatabase, INotifyPropertyChanged
    {
        private string title;
        private string description;

        public string Title
        {
            get => title;
            set
            {
                if (value == title) return;
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Description
        {
            get => description;
            set
            {
                if (value == description) return;
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
