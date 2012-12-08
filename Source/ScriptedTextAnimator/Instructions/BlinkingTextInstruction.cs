using System;
using System.Collections.Generic;
using ScriptedTextAnimator.Rendering;
using ScriptedTextAnimator.ValidationModel;

namespace ScriptedTextAnimator.Instructions
{
    [ScriptedAction("Blinking Text")]
    public class BlinkingTextInstruction : TextInstruction
    {
        [ScriptedProperty("Delay", 1, 1, 1000)]
        public int Delay { get; set; }

        [ScriptedProperty("Length", 100, 1, 1000)]
        public int Length { get; set; }

        public override string DisplayText
        {
            get { return string.Format("Blinking Text: '{0}', Length = '{2}', Delay = '{1}'", Text, Delay, Length); }
        }

        internal override IList<DrawingInstruction> CreateInstructions(ProjectInstructions projectInstructions, IList<ScriptedInstruction> script, IList<DrawingInstruction> currentInstructions)
        {
            var frameCount = Length / Delay;
            frameCount = frameCount % 2 == 0 ? frameCount + 1 : frameCount;

            var previousFrame = PreviousFrame(currentInstructions).Copy();

            var newFrameTemplate = new DrawingInstruction
            {
                Cursor = false,
                Delay = this.Delay,
                Text = string.Concat(previousFrame.Text, this.Text)
            };

            var results = new List<DrawingInstruction>();

            for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
            {
                var on = frameIndex % 2 == 0;

                var newFrame = on ? newFrameTemplate.Copy() : previousFrame;
                results.Add(newFrame);
            }

            return results;
        }
    }
}