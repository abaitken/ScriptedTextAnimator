using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ScriptedTextAnimator.Instructions;

namespace ScriptedTextAnimator.Presentation.Controls
{
    /// <summary>
    ///   Interaction logic for EnumerationEditor.xaml
    /// </summary>
    public partial class EnumerationEditor : INotifyPropertyChanged
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(IInstructionProperty),
                                                                                              typeof(EnumerationEditor),
                                                                                              new PropertyMetadata(OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (EnumerationEditor) d;

            editor.SendPropertyChanged("Items");
            editor.SendPropertyChanged("SelectedItem");
        }

        public IInstructionProperty Value
        {
            get { return (IInstructionProperty)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }


        public EnumerationEditor()
        {
            InitializeComponent();
        }

        public IEnumerable<string> Items
        {
            get
            {
                if(Value == null)
                    return Enumerable.Empty<string>();

                return Enum.GetNames(Value.TargetType);
            }
        }

        public string SelectedItem
        {
            get
            {
                if (Value == null)
                    return string.Empty;

                return Value.Value.ToString();
            }

            set { Value.Value = Enum.Parse(Value.TargetType, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void SendPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}