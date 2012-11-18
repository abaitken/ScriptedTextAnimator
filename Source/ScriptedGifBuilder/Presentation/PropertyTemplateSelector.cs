using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using ScriptedGifBuilder.Instructions;

namespace ScriptedGifBuilder.Presentation
{
    internal class PropertyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StringTemplate { get; set; }
        public DataTemplate ColorTemplate { get; set; }
        public DataTemplate IntegerTemplate { get; set; }
        public DataTemplate BooleanTemplate { get; set; }
        public DataTemplate EnumerationTemplate { get; set; }
        public DataTemplate FlagsEnumerationTemplate { get; set; }
        public DataTemplate UnknownTemplate { get; set; }
        public DataTemplate ComboEditorTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var property = (IInstructionProperty)item;

            if (property.ValueStrategy.IsMultiValue)
                return ComboEditorTemplate;

            if (property.TargetType == typeof(string))
                return StringTemplate;

            if (property.TargetType == typeof(Color))
                return ColorTemplate;

            if (property.TargetType == typeof(int))
                return IntegerTemplate;

            if (property.TargetType == typeof(bool))
                return BooleanTemplate;

            if(property.TargetType.IsEnum)
            {
                var attributes = property.TargetType.GetCustomAttributes<FlagsAttribute>(false).ToArray();

                if (attributes.Length == 0)
                    return EnumerationTemplate;

                return FlagsEnumerationTemplate;
            }

            return UnknownTemplate;
        }
    }
}