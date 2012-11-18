using System;
using ScriptedTextAnimator.Instructions;

namespace ScriptedTextAnimator.Rendering
{
    [Serializable]
    internal class RenderArgs
    {
        private readonly ScriptedInstruction[] script;
        private readonly ProjectInstructions projectInstructions;

        public RenderArgs(ScriptedInstruction[] script, ProjectInstructions projectInstructions)
        {
            this.script = script;
            this.projectInstructions = projectInstructions;
        }

        public ProjectInstructions ProjectInstructions
        {
            get { return projectInstructions; }
        }

        public ScriptedInstruction[] Script
        {
            get { return script; }
        }
    }
}