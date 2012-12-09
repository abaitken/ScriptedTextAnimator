using System.Reflection;

namespace ScriptedTextAnimator.ValueStrategies
{
    internal class IntegerValueStrategy : ComparableValueStrategy<int>
    {
        public IntegerValueStrategy(int defaultValue, int minValue, int maxValue)
            : base(defaultValue, minValue, maxValue)
        {
        }
    }
}