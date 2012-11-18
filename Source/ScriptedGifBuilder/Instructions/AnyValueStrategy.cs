using System;
using System.Windows.Controls;

namespace ScriptedGifBuilder.Instructions
{
    internal class AnyValueStrategy : ValueStrategyBase
    {
        private readonly object defaultValue;

        public AnyValueStrategy(object defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public override object DefaultValue
        {
            get { return defaultValue; }
        }

        protected override Type Type
        {
            get { return DefaultValue.GetType(); }
        }

        protected override ValidationResult ValidateImpl(object value)
        {
            return ValidationResult.ValidResult;
        }
    }
}