using System;
using System.Xml.Serialization;

namespace ScriptedTextAnimator.Project
{
    [XmlRoot("Project")]
    public class BasicProject
    {
        public string SchemaVersion { get; set; }
        public string ProgramVersion { get; set; }
    }
}