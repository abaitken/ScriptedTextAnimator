using System.Collections.Generic;
using ScriptedGifBuilder.Rendering;

namespace ScriptedGifBuilder.Instructions
{
    [ScriptedAction("Typed Text")]
    public class TypedTextInstruction : TextInstruction
    {
        [ScriptedProperty("Delay", 1, 1, 1000)]
        public int Delay { get; set; }

        public override string DisplayText
        {
            get { return string.Format("Typed Text: '{0}', Delay = '{1}'", Text, Delay); }
        }

        internal override IList<DrawingInstruction> CreateInstructions(ProjectInstructions projectInstructions, IList<ScriptedInstruction> script, IList<DrawingInstruction> currentInstructions)
        {
            var results = new List<DrawingInstruction>();

            for (var textIndex = 0; textIndex < Text.Length; textIndex++)
            {
                var previousFrame = results.Count == 0
                                        ? PreviousFrame(currentInstructions)
                                        : results[results.Count - 1];

                var current = string.Concat(previousFrame.Text, Text[textIndex]);

                var newFrame = new DrawingInstruction
                               {
                                   Cursor = projectInstructions.DrawCursor,
                                   Delay = Delay,
                                   Text = current
                               };

                results.Add(newFrame);
            }

            if (projectInstructions.DrawCursor)
            {
                var previousFrame = results[results.Count - 1];
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