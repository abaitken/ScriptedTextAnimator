namespace ScriptedTextAnimator.ValueStrategies
{
    internal class DoubleValueStrategy : ComparableValueStrategy<double>
    {
        public DoubleValueStrategy(double defaultValue, double minValue, double maxValue)
            : base(defaultValue, minValue, maxValue)
        {
        }
    }
}