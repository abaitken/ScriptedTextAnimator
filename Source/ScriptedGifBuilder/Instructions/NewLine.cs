using System;
using System.Collections.Generic;
using System.Drawing;
using ScriptedGifBuilder.Rendering;

namespace ScriptedGifBuilder.Instructions
{
    [ScriptedAction("New Line")]
    public class NewLine : ScriptedInstruction
    {
        public override string DisplayText
        {
            get { return "New Line"; }
        }

        internal override Measure Measure(Graphics g, ProjectInstructions projectInstructions, ScriptedInstruction[] script)
        {
            return new Measure(OriginInstruction.NewRow);
        }

        internal override IList<DrawingInstruction> CreateInstructions(ProjectInstructions projectInstructions, IList<ScriptedInstruction> script, IList<DrawingInstruction> currentInstructions)
        {
            var results = new List<DrawingInstruction>();

            var previousFrame = PreviousFrame(currentInstructions);

            var text = string.Concat(previousFrame.Text, '\n');
            var newFrame = new DrawingInstruction
            {
                Cursor = projectInstructions.DrawCursor,
                Delay = 1,
                Text = text
            };

            results.Add(newFrame);

            var lastFrame = new DrawingInstruction
            {
                Cursor = false,
                Delay = 1,
                Text = text
            };

            results.Add(lastFrame);

            return results;
        }
    }
}