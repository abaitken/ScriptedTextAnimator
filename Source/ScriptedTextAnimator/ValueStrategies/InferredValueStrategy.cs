using System;
using System.Windows.Controls;
using ScriptedTextAnimator.Instructions;

namespace ScriptedTextAnimator.ValueStrategies
{
    internal class InferredValueStrategy<T> : ValueStrategyBase<T>
    {
        private readonly T defaultValue;

        public InferredValueStrategy(T defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public override object DefaultValue
        {
            get { return defaultValue; }
        }
    }

    internal static class InferredValueStrategy
    {
        public static IValueStrategy CreateFrom(object defaultValue)
        {
            var genericTemplate = typeof(InferredValueStrategy<>);
            var typeArgs = new[] { defaultValue.GetType() };
            var genericObjectType = genericTemplate.MakeGenericType(typeArgs);
            var result = (IValueStrategy)Activator.CreateInstance(genericObjectType, defaultValue);

            return result;
        }
    }
}