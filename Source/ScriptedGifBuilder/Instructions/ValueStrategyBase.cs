using System;
using System.Windows.Controls;

namespace ScriptedGifBuilder.Instructions
{
    internal abstract class ValueStrategyBase : IValueStrategy
    {
        public abstract object DefaultValue { get; }
        protected abstract Type Type { get; }

        #region IValueStrategy Members

        public virtual bool IsMultiValue
        {
            get { return false; }
        }

        public virtual object[] Values
        {
            get { throw new InvalidOperationException("Override this property"); }
        }

        #endregion

        public ValidationResult Validate(object value)
        {
            if (value == null)
                return ValidationResult.ValidResult;

            if (value.GetType() != Type)
                return ValidationResult.ValidResult;

            return ValidateImpl(value);
        }

        protected abstract ValidationResult ValidateImpl(object value);
    }
}