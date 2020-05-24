using Packit.Model.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
        public int UserId { get; set; }
        public bool IsChecked { get => isChecked; set => Set(ref isChecked, value); }

        public int GetId() => CheckId;
        public int GetUserId() => UserId;
        public void SetUserId(int value) => UserId = value;
        public void SetId(int value) => CheckId = value;
    }
}
