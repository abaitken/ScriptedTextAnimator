using System.Collections.Generic;
using System.Xml.Serialization;
using ScriptedGifBuilder.Instructions;

namespace ScriptedGifBuilder.Project
{
    public class Project : BasicProject
    {
        public List<ScriptedInstruction> ScriptedInstructions { get; set; }
        public ProjectInstructions ProjectInstructions { get; set; }
        public string OutputFileName { get; set; }
    }
}
