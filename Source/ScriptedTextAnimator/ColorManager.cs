using System.Drawing;
using System.Windows.Forms;

namespace ScriptedTextAnimator
{
    internal static class ColorManager
    {
        private static int[] customColors;

        static ColorManager()
        {
            customColors = new int[0];
        }

        public static Color? Show(Color currentColor)
        {
            var dialog = new ColorDialog
                         {
                             AllowFullOpen = true,
                             AnyColor = true,
                             Color = currentColor,
                             SolidColorOnly = true,
                             CustomColors = customColors
                         };

            var result = dialog.ShowDialog();

            if (result == DialogResult.Cancel)
                return null;

            customColors = dialog.CustomColors;

            return dialog.Color;
        }
    }
}
