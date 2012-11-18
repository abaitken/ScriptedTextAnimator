namespace ScriptedTextAnimator.Presentation.ErrorHandling
{
    /// <summary>
    /// Interaction logic for ExceptionMessageWindow.xaml
    /// </summary>
    internal partial class ExceptionMessageWindow
    {
        public ExceptionMessageWindow()
        {
            InitializeComponent();
        }

        public ExceptionMessageViewModel ViewModel
        {
            get { return DataContext as ExceptionMessageViewModel; }
            set { DataContext = value; }
        }
    }
}
