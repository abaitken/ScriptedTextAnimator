using System.Drawing;

namespace ScriptedGifBuilder.Instructions
{
    internal class BlackDefaultValueStrategy : ColorValueStrategyBase
    {
        public override object DefaultValue
        {
            get { return Color.Black; }
        }
    }
}