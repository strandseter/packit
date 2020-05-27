using Packit.Model.Models;
using Packit.Model.NotifyPropertyChanged;
using System.ComponentModel.DataAnnotations;

namespace Packit.Model
{
    public abstract class BaseInformation : Observable, IDatabase
    {
        private string title;
        private string description;

        [Required]
        [StringLength(20)]
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }
        [StringLength(30)]
        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }

        public string ImageStringName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "<Pending>")]
        public int UserId { get; set; } //I know the naming is a problem. But I did not have time to fix it. Se mye description document.

        public BaseInformation() { }

        public int GetUserId() => UserId;
        public void SetUserId(int value) => UserId = value;
        public abstract int GetId();
        public abstract void SetId(int value);
    }
}
