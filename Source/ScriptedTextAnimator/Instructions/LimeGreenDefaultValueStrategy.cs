using System.Drawing;

namespace ScriptedTextAnimator.Instructions
{
    internal class LimeGreenDefaultValueStrategy : ColorValueStrategyBase
    {
        public override object DefaultValue
        {
            get { return Color.LimeGreen; }
        }
    }
}