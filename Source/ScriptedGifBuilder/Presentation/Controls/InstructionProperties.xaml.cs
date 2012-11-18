﻿using System.Collections.Generic;
using System.Windows;
using ScriptedGifBuilder.Instructions;

namespace ScriptedGifBuilder.Presentation.Controls
{
    /// <summary>
    ///   Interaction logic for InstructionProperties.xaml
    /// </summary>
    internal partial class InstructionProperties
    {
        public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register("Properties",
                                                                                                   typeof(IEnumerable<IInstructionProperty>),
                                                                                                   typeof(InstructionProperties),
                                                                                                   new PropertyMetadata());
        public InstructionProperties()
        {
            InitializeComponent();
        }

        public IEnumerable<IInstructionProperty> Properties
        {
            get { return (IEnumerable<IInstructionProperty>)GetValue(PropertiesProperty); }
            set { SetValue(PropertiesProperty, value); }
        }

    }
}