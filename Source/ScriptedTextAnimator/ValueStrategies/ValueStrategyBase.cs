using System;
using System.Windows.Controls;

namespace ScriptedTextAnimator.ValueStrategies
{
    internal abstract class ValueStrategyBase<T> : IValueStrategy
    {
        public abstract object DefaultValue { get; }

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
                return new ValidationResult(false, "Null value");

            if (value.GetType() != typeof(T))
                return new ValidationResult(false, "Types do not match");

            return ValidateImpl((T)value);
        }

        protected virtual ValidationResult ValidateImpl(T value)
        {
            return ValidationResult.ValidResult;
        }
    }
}