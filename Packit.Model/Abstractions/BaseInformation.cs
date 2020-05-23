using Packit.Model.Models;
using Packit.Model.NotifyPropertyChanged;

namespace Packit.Model
{
    public abstract class BaseInformation : Observable, IDatabase
    {
        private string title;
        private string description;

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }

        public string ImageStringName { get; set; }
        public int UserId { get; set; }

        public BaseInformation() { }

        public int GetUserId() => UserId;
        public void SetUserId(int value) => UserId = value;
        public abstract int GetId();
        public abstract void SetId(int value);
    }
}
