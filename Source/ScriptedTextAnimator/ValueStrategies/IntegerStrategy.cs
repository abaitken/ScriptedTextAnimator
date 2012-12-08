using System.Reflection;

namespace ScriptedTextAnimator.ValueStrategies
{
    internal class IntegerStrategy : ComparableStrategy<int>
    {
        public IntegerStrategy(int defaultValue, int minValue, int maxValue) 
            : base(defaultValue, minValue, maxValue)
        {
        }
    }
}