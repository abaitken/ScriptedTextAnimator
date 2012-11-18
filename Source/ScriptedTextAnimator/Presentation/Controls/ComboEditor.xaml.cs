using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ScriptedTextAnimator.Instructions;

namespace ScriptedTextAnimator.Presentation.Controls
{
    /// <summary>
    ///   Interaction logic for ComboEditor.xaml
    /// </summary>
    public partial class ComboEditor : INotifyPropertyChanged
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value",
                                                                                              typeof (IInstructionProperty),
                                                                                              typeof (ComboEditor),
                                                                                              new PropertyMetadata(OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (ComboEditor) d;
            editor.SendPropertyChanged("SelectedItem");
            editor.SendPropertyChanged("ItemsSource");
        }


        public ComboEditor()
        {
            InitializeComponent();
        }

        public IInstructionProperty Value
        {
            get { return (IInstructionProperty) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }


        public object SelectedItem
        {
            get
            {
                if(Value == null)
                    return null;

                return Value.Value;
            }
            set
            {
                if (SelectedItem == value)
                    return;

                Value.Value = value;
                SendPropertyChanged("SelectedItem");
            }
        }

        public IEnumerable<object> ItemsSource
        {
            get
            {
                if (Value == null)
                    return Enumerable.Empty<object>();

                var valueStrategy = Value.ValueStrategy;
                return valueStrategy.Values;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void SendPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}