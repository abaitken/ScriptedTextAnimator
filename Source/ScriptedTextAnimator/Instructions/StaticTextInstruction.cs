using System.Collections.Generic;
using ScriptedTextAnimator.Rendering;
using ScriptedTextAnimator.ValidationModel;

namespace ScriptedTextAnimator.Instructions
{
    [ScriptedAction("Static Text")]
    public class StaticTextInstruction : TextInstruction
    {
        public override string DisplayText
        {
            get { return string.Format("Static Text: '{0}'", Text); }
        }

        internal override IList<DrawingInstruction> CreateInstructions(ProjectInstructions projectInstructions, IList<ScriptedInstruction> script, IList<DrawingInstruction> currentInstructions)
        {
            var results = new List<DrawingInstruction>();

            var previousFrame = PreviousFrame(currentInstructions);

            var newFrame = new DrawingInstruction
                           {
                               Cursor = projectInstructions.DrawCursor,
                               Delay = 1,
                               Text = string.Concat(previousFrame.Text, Text)
                           };

            results.Add(newFrame);

            return results;
        }
    }
}