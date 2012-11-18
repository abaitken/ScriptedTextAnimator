using System.Drawing;
using ScriptedGifBuilder.Rendering;

namespace ScriptedGifBuilder.Instructions
{
    public abstract class TextInstruction : ScriptedInstruction
    {
        [ScriptedProperty("Text", "")]
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