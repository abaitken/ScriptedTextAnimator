using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using ScriptedTextAnimator.Instructions;
using ScriptedTextAnimator.ValidationModel;
using Size = System.Windows.Size;

namespace ScriptedTextAnimator.Presentation.Controls
{
    /// <summary>
    ///   Interaction logic for ColorEditor.xaml
    /// </summary>
    public partial class ColorEditor : INotifyPropertyChanged
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value",
                                                                                              typeof (IInstructionProperty),
                                                                                              typeof (ColorEditor),
                                                                                              new PropertyMetadata(OnValueChanged));
        public ColorEditor()
        {
            InitializeComponent();
        }

        public IInstructionProperty Value
        {
            get { return (IInstructionProperty) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public Color Color
        {
            get
            {
                if (Value == null)
                    return Color.White;

                return (Color) Value.Value;
            }
            set
            {
                if (Color == value)
                    return;

                Value.Value = value;
                SendPropertyChanged("Color");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (ColorEditor) d;
            editor.SendPropertyChanged("Color");
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var baseResult = base.MeasureOverride(constraint);

            var width = Math.Max(baseResult.Width, constraint.Width);
            var height = Math.Max(baseResult.Height, constraint.Height);

            if (double.IsInfinity(width) || width < MinWidth)
                width = MinWidth;

            if (double.IsInfinity(height) || height < MinHeight)
                height = MinHeight;

            return new Size(width, height);
        }

        private void OnChangeColor(object sender, RoutedEventArgs e)
        {
            var result = ColorManager.Show(Color);

            if (!result.HasValue)
                return;

            Color = result.Value;
        }

        private void SendPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}