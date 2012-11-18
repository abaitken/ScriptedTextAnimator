using System.Drawing;

namespace ScriptedTextAnimator.Instructions
{
    internal class DarkGreenDefaultValueStrategy : ColorValueStrategyBase
    {
        public override object DefaultValue
        {
            get { return Color.DarkGreen; }
        }
    }
}