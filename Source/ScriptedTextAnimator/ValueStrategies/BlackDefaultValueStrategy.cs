using System.Drawing;
using ScriptedTextAnimator.Instructions;

namespace ScriptedTextAnimator.ValueStrategies
{
    internal class BlackDefaultValueStrategy : ColorValueStrategyBase
    {
        public override object DefaultValue
        {
            get { return Color.Black; }
        }
    }
}