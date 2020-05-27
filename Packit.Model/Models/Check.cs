using Packit.Model.NotifyPropertyChanged;

namespace Packit.Model.Models
{
    public class Check : Observable, IDatabase
    {
        private bool isChecked;

        public int CheckId { get; set; }
        public int TripId { get; set; }
        public int BackpackId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "<Pending>")]
        public int UserId { get; set; } //I know the naming is an issue. See the document.
        public bool IsChecked { get => isChecked; set => Set(ref isChecked, value); }

        public int GetId() => CheckId;
        public int GetUserId() => UserId;
        public void SetUserId(int value) => UserId = value;
        public void SetId(int value) => CheckId = value;
    }
}
