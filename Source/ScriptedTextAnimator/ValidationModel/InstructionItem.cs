using System;

namespace ScriptedTextAnimator.ValidationModel
{
    internal class InstructionItem
    {
        private readonly Type type;
        private readonly string name;

        public InstructionItem(Type type, string name)
        {
            this.type = type;
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public Type Type
        {
            get { return type; }
        }
    }
}