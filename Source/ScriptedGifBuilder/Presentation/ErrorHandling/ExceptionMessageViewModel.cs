using System.Windows.Input;
using TemperedSoftware.Shared.Presentation.Commands;

namespace ScriptedGifBuilder.Presentation.ErrorHandling
{
    class ExceptionMessageViewModel
    {
        private readonly ExceptionMessageWindow view;
        private readonly System.Exception exception;
        private readonly string message;

        public ExceptionMessageViewModel(ExceptionMessageWindow view, System.Exception exception, string message)
        {
            this.view = view;
            this.exception = exception;
            this.message = message;
            CreateCommands();
        }

        public ExceptionMessageViewModel(ExceptionMessageWindow view, System.Exception exception)
            : this(view, exception, string.Empty)
        {
        }

        private void CreateCommands()
        {
            CloseCommand = new DelegateCommand(OnClose);
            ShowDetailCommand = new DelegateCommand(OnShowDetail);
        }

        private void OnShowDetail()
        {
            var detailView = new ExceptionDetailWindow();
            var detailViewModel = new ExceptionDetailViewModel(detailView, exception);
            detailView.ViewModel = detailViewModel;
            detailView.Owner = view;
            detailView.ShowDialog();
        }

        private void OnClose()
        {
            view.Close();
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ShowDetailCommand { get; set; }

        public string WindowTitle
        {
            get { return exception.GetType().ToString(); }
        }

        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(message))
                    return exception.Message;

                return string.Format("{0} {1}", message, exception.Message);
            }
        }
    }
}
