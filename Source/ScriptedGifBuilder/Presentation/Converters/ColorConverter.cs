using System.Windows.Media;
using TemperedSoftware.Shared.Presentation.Converters;

namespace ScriptedGifBuilder.Presentation.Converters
{
    class ColorConverter : ConverterBase<System.Drawing.Color, Color>
    {
        protected override Color ConvertImpl(System.Drawing.Color value)
        {
            return Color.FromArgb(value.A, value.R, value.G, value.B);
        }

        protected override System.Drawing.Color ConvertBackImpl(Color value)
        {
            return System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
        }
    }
}