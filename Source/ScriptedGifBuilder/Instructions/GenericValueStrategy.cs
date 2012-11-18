using System;
using System.Reflection;
using System.Windows.Controls;

namespace ScriptedGifBuilder.Instructions
{
    internal class GenericValueStrategy : ValueStrategyBase
    {
        private readonly object defaultValue;
        private readonly object maxValue;
        private readonly object minValue;

        public GenericValueStrategy(object defaultValue, object minValue, object maxValue)
        {
            if (!defaultValue.GetType().IsValueType)
                throw new InvalidOperationException("Values must be a value type");

            var typesMatch = defaultValue.GetType() == minValue.GetType() &&
                             defaultValue.GetType() == maxValue.GetType();

            if (!typesMatch)
                throw new InvalidOperationException("Values do not match");


            this.defaultValue = defaultValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
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
            var minComparison =
                (int) Type.InvokeMember("CompareTo", BindingFlags.InvokeMethod, null, value, new[] {minValue});
            var maxComparison =
                (int) Type.InvokeMember("CompareTo", BindingFlags.InvokeMethod, null, value, new[] {maxValue});

            var isValid = minComparison >= 0 && maxComparison <= 0;

            if (isValid)
                return ValidationResult.ValidResult;

            return new ValidationResult(false, string.Format("Value must be between {0} and {1}", minValue, maxValue));
        }
    }
}