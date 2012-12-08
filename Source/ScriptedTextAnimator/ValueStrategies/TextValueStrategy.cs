using System;
using System.Windows.Controls;

namespace ScriptedTextAnimator.ValueStrategies
{
    class TextValueStrategy : ValueStrategyBase<string>
    {
        public override object DefaultValue
        {
            get { return string.Empty; }
        }

        protected override ValidationResult ValidateImpl(string value)
        {
            if (string.IsNullOrEmpty(value))
                return new ValidationResult(false, "Empty string is not valid");

            return ValidationResult.ValidResult;
        }
    }
}