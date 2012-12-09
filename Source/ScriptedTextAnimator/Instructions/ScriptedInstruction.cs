using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using ScriptedTextAnimator.Rendering;

namespace ScriptedTextAnimator.Instructions
{
    [XmlInclude(typeof (TextInstruction))]
    [XmlInclude(typeof (TypedTextInstruction))]
    [XmlInclude(typeof (StaticTextInstruction))]
    [XmlInclude(typeof (BlinkingTextInstruction))]
    [XmlInclude(typeof (DelayInstruction))]
    [XmlInclude(typeof (NewLine))]
    [XmlInclude(typeof (ProjectInstructions))]
    [XmlInclude(typeof (WipeFrame))]
    [XmlInclude(typeof (CounterInstruction))]
    public abstract class ScriptedInstruction
    {
        public abstract string DisplayText { get; }
        internal abstract Measure Measure(Graphics g, ProjectInstructions projectInstructions, ScriptedInstruction[] script);

        internal abstract IList<DrawingInstruction> CreateInstructions(ProjectInstructions projectInstructions, IList<ScriptedInstruction> script, IList<DrawingInstruction> currentInstructions);

        internal DrawingInstruction PreviousFrame(IList<DrawingInstruction> currentInstructions)
        {
            return currentInstructions[currentInstructions.Count - 1];
        }
    }
}