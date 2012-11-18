using System.Windows.Forms;
using System.Windows.Input;
using TemperedSoftware.Shared.Presentation.Commands;

namespace ScriptedTextAnimator.Presentation.ErrorHandling
{
    class ExceptionDetailViewModel
    {
        private readonly ExceptionDetailWindow view;
        private readonly System.Exception exception;

        public ExceptionDetailViewModel(ExceptionDetailWindow view, System.Exception exception)
        {
            this.view = view;
            this.exception = exception;
            CopyToClipboardCommand = new DelegateCommand(OnCopyToClipboard);
            CloseCommand = new DelegateCommand(OnClose);
        }

        private void OnClose()
        {
            view.Close();
        }

        public ICommand CloseCommand { get; set; }
        public ICommand CopyToClipboardCommand { get; set; }

        public string Detail
        {
            get
            {
                return string.Format(@"{2}{0}
{1}", exception.Message, exception.StackTrace, exception.GetType());
            }
        }

        private void OnCopyToClipboard()
        {
            try
            {
                Clipboard.SetText(Detail);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format(@"Failed to copy to clipboard:
{0}", ex.Message));
            }
        }
    }
}
