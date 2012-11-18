namespace ScriptedTextAnimator.Presentation.ErrorHandling
{
    /// <summary>
    /// Interaction logic for ExceptionDetailWindow.xaml
    /// </summary>
    internal partial class ExceptionDetailWindow
    {
        public ExceptionDetailWindow()
        {
            InitializeComponent();
        }

        public ExceptionDetailViewModel ViewModel
        {
            get { return DataContext as ExceptionDetailViewModel; }
            set { DataContext = value; }
        }
    }
}
