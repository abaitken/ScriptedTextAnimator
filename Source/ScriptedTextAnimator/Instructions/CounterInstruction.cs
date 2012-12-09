using System;
using System.Collections.Generic;
using System.Drawing;
using ScriptedTextAnimator.Rendering;
using ScriptedTextAnimator.ValidationModel;

namespace ScriptedTextAnimator.Instructions
{
    [ScriptedAction("Counter Action")]
    public class CounterInstruction : ScriptedInstruction
    {
        [ScriptedProperty("Start", 1.0)]
        public double Start { get; set; }
        [ScriptedProperty("End", 1.0)]
        public double End { get; set; }
        [ScriptedProperty("Change", 1.0)]
        public double Change { get; set; }
        [ScriptedProperty("Leave text when finished", false)]
        public bool LeaveTextWhenFinished { get; set; }
        [ScriptedProperty("Delay", 1, 1, 1000)]
        public int Delay { get; set; }

        public override string DisplayText
        {
            get { return string.Format("Counter. Starts at {0} ends at {1} in increments of {2}.", Start, End, Change); }
        }

        internal override Measure Measure(Graphics g, ProjectInstructions projectInstructions, ScriptedInstruction[] script)
        {
            var startText = (Start + Change).ToString();
            var endText = (End + Change).ToString();
            var text = startText.Length > endText.Length ? startText : endText;

            var font = RenderingHelper.CreateFont(g, projectInstructions.FontFamily, projectInstructions.FontStyle,
                                                  projectInstructions.FontSize);

            var size = g.MeasureString(text, font).ToSize();

            return new Measure(size);
        }

        internal override IList<DrawingInstruction> CreateInstructions(ProjectInstructions projectInstructions, IList<ScriptedInstruction> script, IList<DrawingInstruction> currentInstructions)
        {
            var results = new List<DrawingInstruction>();

            if (Change == 0 || Start == End)
                return results;

            var realChange = GetRealChange();

            var previousFrame = PreviousFrame(currentInstructions);
            for (var current = Start; End > Start && current <= End || Start > End && current >= End; current += realChange)
            {
                var newFrame = new DrawingInstruction
                {
                    Cursor = false,
                    Delay = Delay,
                    Text = string.Concat(previousFrame.Text, current.ToString())
                };

                results.Add(newFrame);
            }

            if (!LeaveTextWhenFinished)
                results.Add(previousFrame.Copy());

            return results;
        }

        private double GetRealChange()
        {
            var positiveChange = Math.Abs(Change);
            var negativeChange = positiveChange*-1;

            if (End > Start)
                return positiveChange;

            return negativeChange;
        }
    }
}