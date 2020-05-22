using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Packit.App.Convertes
{
    public class BooleanToTextConverter : IValueConverter
    {
        public string TrueString { get; set; }
        public string FalseString { get; set; }
        public bool IsReversed { get; set; }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = System.Convert.ToBoolean(value); //TODO: Fix warning

            if (IsReversed)
                val = !val;

            if (val)
                return TrueString;

            return FalseString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
