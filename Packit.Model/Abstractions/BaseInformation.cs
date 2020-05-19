using Packit.Model.Abstractions;
using Packit.Model.Models;

namespace Packit.Model
{
    public abstract class BaseInformation : BaseModel, IDatabase
    {
        private string title;
        private string description;

        public string Title
        {
            get => title;
            set 
            {
                if (!Equals(title, value))
                {
                    if (string.IsNullOrEmpty(value))
                        AddError(nameof(Title), "Title cannot be empty");
                    else if (value.Length > 15)
                        AddError(nameof(Title), "Title cannot be longer than 15 digits");
                    else
                        RemoveError(nameof(Title));
                }
                Set(ref title, value);
            }
        }

        public string Description
        {
            get => description;
            set 
            {
                if (!Equals(description, value))
                {
                    if (value?.Length > 30)
                        AddError(nameof(Description), "Description cannot be longer than 30 digits");
                    else
                        RemoveError(nameof(Description));
                }
                Set(ref description, value);
            } 
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
