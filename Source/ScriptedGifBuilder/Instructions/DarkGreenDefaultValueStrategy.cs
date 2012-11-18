using System.Drawing;

namespace ScriptedGifBuilder.Instructions
{
    internal class DarkGreenDefaultValueStrategy : ColorValueStrategyBase
    {
        public override object DefaultValue
        {
            get { return Color.DarkGreen; }
        }
    }
}