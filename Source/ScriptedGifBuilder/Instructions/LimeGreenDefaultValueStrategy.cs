using System.Drawing;

namespace ScriptedGifBuilder.Instructions
{
    internal class LimeGreenDefaultValueStrategy : ColorValueStrategyBase
    {
        public override object DefaultValue
        {
            get { return Color.LimeGreen; }
        }
    }
}