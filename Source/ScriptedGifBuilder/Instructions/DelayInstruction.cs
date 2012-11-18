using System;
using System.Collections.Generic;
using System.Drawing;
using ScriptedGifBuilder.Rendering;

namespace ScriptedGifBuilder.Instructions
{
    [ScriptedAction("Delay")]
    public class DelayInstruction : ScriptedInstruction
    {
        [ScriptedProperty("Delay", 1, 1, 1000)]
        public int Delay { get; set; }

        [ScriptedProperty("Length", 100, 1, 1000)]
        public int Length { get; set; }

        [ScriptedProperty("Flash Cursor", false)]
        public bool FlashCursor { get; set; }

        [ScriptedProperty("Draw Cursor", false)]
        public bool DrawCursor { get; set; }

        public override string DisplayText
        {
            get { return string.Format("Delay: '{0}', Length = '{1}'", Delay, Length); }
        }

        internal override Measure Measure(Graphics g, ProjectInstructions projectInstructions, ScriptedInstruction[] script)
        {
            if (!DrawCursor)
                return new Measure();

            var font = RenderingHelper.CreateFont(g, projectInstructions.FontFamily, projectInstructions.FontStyle,
                                                  projectInstructions.FontSize);

            var size = g.MeasureString("_", font).ToSize();

            return new Measure(size);
        }

        internal override IList<DrawingInstruction> CreateInstructions(ProjectInstructions projectInstructions, IList<ScriptedInstruction> script, IList<DrawingInstruction> currentInstructions)
        {
            var results = new List<DrawingInstruction>();
            var previousFrame = PreviousFrame(currentInstructions);

            if (!FlashCursor)
            {
                var newFrame = new DrawingInstruction
                {
                    Cursor = DrawCursor,
                    Delay = Length,
                    Text = previousFrame.Text
                };

                results.Add(newFrame);

                if (DrawCursor)
                {
                    var newFrame2 = new DrawingInstruction
                    {
                        Cursor = false,
                        Delay = Length,
                        Text = previousFrame.Text
                    };

                    results.Add(newFrame2);
                }
            }
            else
            {
                var frameCount = Length / Delay;
                frameCount = frameCount % 2 == 0 ? frameCount : frameCount + 1;

                var newFrameTemplate = new DrawingInstruction
                {
                    Cursor = true,
                    Delay = Delay,
                    Text = previousFrame.Text
                };

                for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
                {
                    var on = frameIndex % 2 == 0;

                    var newFrame = on ? newFrameTemplate.Copy() : previousFrame.Copy();
                    results.Add(newFrame);
                }

                var lastFrame = new DrawingInstruction
                {
                    Cursor = false,
                    Delay = Delay,
                    Text = previousFrame.Text
                };

                results.Add(lastFrame);
            }

            return results;
        }
    }
}