using System.Drawing;

namespace ScriptedTextAnimator.Instructions
{
    internal class BlackDefaultValueStrategy : ColorValueStrategyBase
    {
        public override object DefaultValue
        {
            get { return Color.Black; }
        }
    }
}