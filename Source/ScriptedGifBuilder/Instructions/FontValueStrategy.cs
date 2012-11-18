using System;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Controls;

namespace ScriptedGifBuilder.Instructions
{
    internal class FontValueStrategy : ValueStrategyBase
    {
        public override object DefaultValue
        {
            get { return "Courier New"; }
        }

        protected override Type Type
        {
            get { return typeof (string); }
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

        protected override ValidationResult ValidateImpl(object value)
        {
            return ValidationResult.ValidResult;
        }
    }
}