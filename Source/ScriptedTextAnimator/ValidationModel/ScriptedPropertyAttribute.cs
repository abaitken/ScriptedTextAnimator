using System;
using ScriptedTextAnimator.ValueStrategies;

namespace ScriptedTextAnimator.ValidationModel
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    internal class ScriptedPropertyAttribute : Attribute
    {
        private readonly string displayName;
        private readonly IValueStrategy valueStrategy;

        public ScriptedPropertyAttribute(string displayName, Type valueStrategy)
        {
            this.displayName = displayName;

            if (!typeof(IValueStrategy).IsAssignableFrom(valueStrategy))
                throw new InvalidOperationException("valueStrategy must be type of IValueStrategy");

            this.valueStrategy = (IValueStrategy)Activator.CreateInstance(valueStrategy);
        }

        public ScriptedPropertyAttribute(string displayName, int defaultValue, int minValue, int maxValue)
        {
            this.displayName = displayName;

            valueStrategy = new IntegerStrategy(defaultValue, minValue, maxValue);
        }

        public ScriptedPropertyAttribute(string displayName, object defaultValue)
        {
            this.displayName = displayName;

            valueStrategy = InferredValueStrategy.CreateFrom(defaultValue);
        }

        public IValueStrategy ValueStrategy
        {
            get { return valueStrategy; }
        }

        public string DisplayName
        {
            get { return displayName; }
        }
    }
}