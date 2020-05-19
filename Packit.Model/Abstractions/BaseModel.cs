using Packit.Model.NotifyPropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Packit.Model.Abstractions
{
    public class BaseModel : Observable, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, IList<string>> errors = new Dictionary<string, IList<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => errors.Count > 0;
        
        public IEnumerable GetErrors(string propertyName)
        {
            if (errors.ContainsKey(propertyName))
                return errors[propertyName];
            return null;
        }

        public void AddError(string propertyName, string error)
        {
            errors[propertyName] = new List<string>() { error };
            NotifyErrorsChanged(propertyName);
        }

        public void RemoveError(string propertyName)
        {
            if (errors.ContainsKey(propertyName))
                errors.Remove(propertyName);
            NotifyErrorsChanged(propertyName);
        }

        public void NotifyErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}
