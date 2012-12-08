using System;
using System.Windows.Controls;

namespace ScriptedTextAnimator.ValueStrategies
{
    internal abstract class ComparableStrategy<T> : ValueStrategyBase<T>
        where T : IComparable<T>
    {
        private readonly T defaultValue;
        private readonly T maxValue;
        private readonly T minValue;

        protected ComparableStrategy(T defaultValue, T minValue, T maxValue)
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

        protected override ValidationResult ValidateImpl(T value)
        {
            var minComparison = value.CompareTo(minValue);
            var maxComparison = value.CompareTo(maxValue);

            var isValid = minComparison >= 0 && maxComparison <= 0;

            if (isValid)
                return ValidationResult.ValidResult;

            return new ValidationResult(false, string.Format("Value must be between {0} and {1}", minValue, maxValue));
        }
        
    }
}