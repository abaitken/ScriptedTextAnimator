using System;
using System.Drawing;
using System.Windows.Controls;

namespace ScriptedGifBuilder.Instructions
{
    internal abstract class ColorValueStrategyBase : ValueStrategyBase
    {
        protected override Type Type
        {
            get { return typeof (Color); }
        }

        protected override ValidationResult ValidateImpl(object value)
        {
            return ValidationResult.ValidResult;
        }
    }
}