using System.Drawing;

namespace ScriptedTextAnimator.ValueStrategies
{
    internal class DarkGreenDefaultValueStrategy : ColorValueStrategyBase
    {
        public override object DefaultValue
        {
            get { return Color.DarkGreen; }
        }
    }
}