using System;
using System.Collections.Generic;
using System.Drawing;
using ScriptedTextAnimator.Rendering;
using ScriptedTextAnimator.ValidationModel;

namespace ScriptedTextAnimator.Instructions
{
    [ScriptedAction("Wipe Frame")]
    public class WipeFrame : ScriptedInstruction
    {
        public override string DisplayText
        {
            get { return "Wipe Frame"; }
        }

        internal override Measure Measure(Graphics g, ProjectInstructions projectInstructions, ScriptedInstruction[] script)
        {
            return new Measure(OriginInstruction.ResetOrigin);
        }

        internal override IList<DrawingInstruction> CreateInstructions(ProjectInstructions projectInstructions, IList<ScriptedInstruction> script, IList<DrawingInstruction> currentInstructions)
        {
            var results = new List<DrawingInstruction>();
            var newFrame = new DrawingInstruction
            {
                Cursor = false,
                Delay = 1,
                Text = string.Empty
            };
            results.Add(newFrame);

            return results;
        }
    }
}