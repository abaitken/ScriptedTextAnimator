using System.Drawing;

namespace ScriptedTextAnimator.ValueStrategies
{
    internal class LimeGreenDefaultValueStrategy : ColorValueStrategyBase
    {
        public override object DefaultValue
        {
            get { return Color.LimeGreen; }
        }
    }
}