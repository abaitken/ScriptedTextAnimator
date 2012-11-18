using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ScriptedTextAnimator.Instructions;

namespace ScriptedTextAnimator.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for FlagsEnumerationEditor.xaml
    /// </summary>
    public partial class FlagsEnumerationEditor : INotifyPropertyChanged
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(IInstructionProperty),
                                                                                              typeof(FlagsEnumerationEditor),
                                                                                              new PropertyMetadata(OnValueChanged));

        private IEnumerable<CheckListItem> checkListItems;

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (FlagsEnumerationEditor) d;

            editor.SendPropertyChanged("Items");
        }

        public IInstructionProperty Value
        {
            get { return (IInstructionProperty)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public FlagsEnumerationEditor()
        {
            InitializeComponent();
        }

        public IEnumerable<CheckListItem> Items
        {
            get
            {
                if (Value == null)
                    return Enumerable.Empty<CheckListItem>();

                if (checkListItems == null)
                {
                    var value = (int) Value.Value;
                    checkListItems = (from item in Enum.GetNames(Value.TargetType)
                                     let memberValue = (int) Enum.Parse(Value.TargetType, item)
                                     where memberValue > 0
                                     let isChecked = (value & memberValue) == memberValue
                                     select new CheckListItem(this, item, isChecked)).ToList();
                }
                return checkListItems;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void SendPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            var result = 0;

            foreach (var checkListItem in checkListItems)
            {
                if(!checkListItem.IsChecked)
                    continue;

                var memberValue = (int) Enum.Parse(Value.TargetType, checkListItem.Name);

                result = result | memberValue;
            }

            Value.Value = result;
        }
    }

    public class CheckListItem
    {
        private readonly FlagsEnumerationEditor parent;
        private readonly string name;
        private bool isChecked;

        public CheckListItem(FlagsEnumerationEditor parent, string name, bool isChecked)
        {
            this.parent = parent;
            this.name = name;
            this.isChecked = isChecked;
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                parent.Update();
            }
        }

        public string Name
        {
            get { return name; }
        }
    }
}
