using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ScriptedTextAnimator.ValidationModel;

namespace ScriptedTextAnimator.Instructions
{
    class InstructionFactory
    {
        private readonly Type type;

        public InstructionFactory(Type type)
        {
            this.type = type;
        }

        public IEnumerable<KeyValuePair<PropertyInfo, ScriptedPropertyAttribute>> GetScriptedPropertyInformation()
        {
            var results = new List<KeyValuePair<PropertyInfo, ScriptedPropertyAttribute>>();

            var properties = type.GetProperties();

            foreach (var propertyInfo in properties)
            {
                var attributes = propertyInfo.GetCustomAttributes<ScriptedPropertyAttribute>(true).ToArray();

                if (attributes.Length == 0)
                    continue;

                var propertyAttribute = attributes[0];
                results.Add(new KeyValuePair<PropertyInfo, ScriptedPropertyAttribute>(propertyInfo, propertyAttribute));
            }

            return results;
        }

        public ScriptedInstruction Create()
        {
            var instruction = (ScriptedInstruction)Activator.CreateInstance(type);

            var properties = GetScriptedPropertyInformation();

            foreach (var item in properties)
            {
                var propertyInfo = item.Key;
                var propertyAttribute = item.Value;

                propertyInfo.SetValue(instruction, propertyAttribute.ValueStrategy.DefaultValue);
            }

            return instruction;
        }
    }
}
