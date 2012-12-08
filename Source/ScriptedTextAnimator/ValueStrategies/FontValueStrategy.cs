using System.Drawing.Text;
using System.Linq;

namespace ScriptedTextAnimator.ValueStrategies
{
    internal class FontValueStrategy : ValueStrategyBase<string>
    {
        public override object DefaultValue
        {
            get { return "Courier New"; }
        }

        public override bool IsMultiValue
        {
            get { return true; }
        }

        public override object[] Values
        {
            get
            {
                var fonts = new InstalledFontCollection();
                var results = from item in fonts.Families
                              select item.Name as object;

                return results.ToArray();
            }
        }
    }
}