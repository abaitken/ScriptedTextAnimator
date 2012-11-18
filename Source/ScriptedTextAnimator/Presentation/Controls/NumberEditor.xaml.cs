using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ScriptedTextAnimator.Instructions;

namespace ScriptedTextAnimator.Presentation.Controls
{
    /// <summary>
    ///   Interaction logic for NumberEditor.xaml
    /// </summary>
    public partial class NumberEditor : INotifyPropertyChanged
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value",
                                                                                              typeof (
                                                                                                  IInstructionProperty),
                                                                                              typeof (NumberEditor),
                                                                                              new PropertyMetadata(
                                                                                                  OnValueChanged));

        public NumberEditor()
        {
            InitializeComponent();
        }

        public IInstructionProperty Value
        {
            get { return (IInstructionProperty) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public string Number
        {
            get
            {
                if (Value == null)
                    return "0";

                return Value.Value.ToString();
            }
            set
            {
                if (Number == value)
                    return;

                Value.Value = int.Parse(value);
                SendPropertyChanged("Number");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NumberEditor) d;
            editor.SendPropertyChanged("Number");
        }

        private void SendPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox) sender;
            textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}