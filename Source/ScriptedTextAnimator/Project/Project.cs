using System.Collections.Generic;
using System.Xml.Serialization;
using ScriptedTextAnimator.Instructions;

namespace ScriptedTextAnimator.Project
{
    public class Project : BasicProject
    {
        public List<ScriptedInstruction> ScriptedInstructions { get; set; }
        public ProjectInstructions ProjectInstructions { get; set; }
        public string OutputFileName { get; set; }
    }
}
