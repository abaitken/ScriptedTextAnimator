using System;
using System.Globalization;
using System.Windows.Data;

namespace ScriptedTextAnimator.Presentation.Converters
{
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (typeof(System.Drawing.Color) != value.GetType())
                throw new InvalidOperationException("Value does not match from type");

            if (typeof(System.Windows.Media.Color) != targetType)
                throw new InvalidOperationException("Target type does not match to type");

            var from = (System.Drawing.Color)value;
            return System.Windows.Media.Color.FromArgb(from.A, from.R, from.G, from.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (typeof(System.Windows.Media.Color) != value.GetType())
                throw new InvalidOperationException("Value does not match to type");

            if (typeof(System.Drawing.Color) != targetType)
                throw new InvalidOperationException("Target type does not match from type");

            var from = (System.Windows.Media.Color)value;
            return System.Drawing.Color.FromArgb(from.A, from.R, from.G, from.B);
        }
    }
}