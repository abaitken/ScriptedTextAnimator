using System;
using System.ComponentModel;
using ScriptedTextAnimator.Rendering;

namespace ScriptedTextAnimator.Background
{
    abstract class BasicBackgroundTask<TFinished>
        where TFinished : BackgroundTaskFinishedArgs
    {
        private readonly BackgroundWorker worker;

        protected BasicBackgroundTask()
        {
            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += OnRunWorkerCompleted;
            worker.DoWork += DoWork;
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = DoWorkImpl();
            }
            catch (Exception ex)
            {
                e.Result = CreateExceptionResult(ex);
            }
        }

        protected abstract TFinished CreateExceptionResult(Exception exception);

        protected abstract TFinished DoWorkImpl();

        protected bool IsBusy
        {
            get { return worker.IsBusy; }
        }

        public event EventHandler<TFinished> TaskFinished;

        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnRunWorkerCompletedImpl((TFinished)e.Result);
        }

        protected virtual void OnRunWorkerCompletedImpl(TFinished result)
        {
            PostTaskFinished(result);
        }

        protected void PostTaskFinished(TFinished args)
        {
            if (TaskFinished != null)
                TaskFinished(this, args);
        }

        protected void RunWorker()
        {
            worker.RunWorkerAsync();
        }
    }
}