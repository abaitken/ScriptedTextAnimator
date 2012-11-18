using System.Drawing;

namespace ScriptedGifBuilder.Instructions
{
    public class Measure
    {
        private readonly OriginInstruction originInstruction;
        private readonly Size? size;

        public Measure(Size size)
        {
            this.size = size;
        }

        public Measure()
        {
        }

        public Measure(OriginInstruction originInstruction)
        {
            this.originInstruction = originInstruction;
        }

        public OriginInstruction OriginInstruction
        {
            get { return originInstruction; }
        }

        public Size? Size
        {
            get { return size; }
        }
    }
}