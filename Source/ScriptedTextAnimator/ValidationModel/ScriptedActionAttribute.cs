using System;

namespace ScriptedTextAnimator.ValidationModel
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class ScriptedActionAttribute : Attribute
    {
        private readonly string name;

        public ScriptedActionAttribute(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }
    }
}