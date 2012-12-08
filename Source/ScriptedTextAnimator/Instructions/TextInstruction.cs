using System.Drawing;
using ScriptedTextAnimator.Rendering;
using ScriptedTextAnimator.ValidationModel;
using ScriptedTextAnimator.ValueStrategies;

namespace ScriptedTextAnimator.Instructions
{
    public abstract class TextInstruction : ScriptedInstruction
    {
        [ScriptedProperty("Text", typeof(TextValueStrategy))]
        public string Text { get; set; }

        internal override Measure Measure(Graphics g, ProjectInstructions projectInstructions, ScriptedInstruction[] script)
        {
            var font = RenderingHelper.CreateFont(g, projectInstructions.FontFamily, projectInstructions.FontStyle,
                                                  projectInstructions.FontSize);

            // TODO : Can reduce size if only using typed text with draw cursor
            var text = string.Concat(Text, projectInstructions.DrawCursor ? "_" : string.Empty);

            var size = g.MeasureString(text, font).ToSize();

            return new Measure(size);
        }
    }
}