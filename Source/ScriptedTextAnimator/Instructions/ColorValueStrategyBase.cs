using System;
using System.Drawing;
using System.Windows.Controls;

namespace ScriptedTextAnimator.Instructions
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