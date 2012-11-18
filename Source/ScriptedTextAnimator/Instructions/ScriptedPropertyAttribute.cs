using System;

namespace ScriptedTextAnimator.Instructions
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    internal class ScriptedPropertyAttribute : Attribute
    {
        private readonly string displayName;
        private readonly ValueStrategyBase valueStrategy;

        public ScriptedPropertyAttribute(string displayName, Type valueStrategy)
        {
            this.displayName = displayName;

            if (!valueStrategy.IsSubclassOf(typeof (ValueStrategyBase)))
                throw new InvalidOperationException("valueStrategy must be type of ValueStrategyBase");

            this.valueStrategy = (ValueStrategyBase) Activator.CreateInstance(valueStrategy);
        }

        public ScriptedPropertyAttribute(string displayName, object defaultValue, object minValue, object maxValue)
        {
            this.displayName = displayName;

            valueStrategy = new GenericValueStrategy(defaultValue, minValue, maxValue);
        }

        public ScriptedPropertyAttribute(string displayName, object defaultValue)
        {
            this.displayName = displayName;

            valueStrategy = new AnyValueStrategy(defaultValue);
        }

        public ValueStrategyBase ValueStrategy
        {
            get { return valueStrategy; }
        }

        public string DisplayName
        {
            get { return displayName; }
        }
    }
}