using System;
using System.ComponentModel;
using ScriptedGifBuilder.Rendering;

namespace ScriptedGifBuilder.Background
{
    abstract class BackgroundTaskWithProgress<TFinished, TArgs>
        where TFinished : BackgroundTaskFinishedArgs
    {
        private readonly BackgroundWorker worker;

        protected BackgroundTaskWithProgress(bool reportsProgress, bool supportsCancellation)
        {
            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += OnRunWorkerCompleted;
            worker.DoWork += DoWork;
            
            worker.WorkerReportsProgress = reportsProgress;
            worker.ProgressChanged += OnProgressChanged;

            worker.WorkerSupportsCancellation = supportsCancellation;
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = DoWorkImpl((TArgs)e.Argument);
            }
            catch (Exception ex)
            {
                e.Result = CreateExceptionResult(ex);
            }
        }

        protected abstract TFinished CreateExceptionResult(Exception exception);

        protected abstract TFinished DoWorkImpl(TArgs args);

        protected bool IsBusy
        {
            get { return worker.IsBusy; }
        }

        public event EventHandler<BackgroundTaskProgressArgs> ProgressUpdate;
        public event EventHandler<TFinished> TaskFinished;

        protected void ReportProgress(int precentageComplete, BackgroundTaskProgressArgs progressArgs)
        {
            worker.ReportProgress(0, progressArgs);
        }

        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnRunWorkerCompletedImpl((TFinished)e.Result);
        }

        protected virtual void OnRunWorkerCompletedImpl(TFinished result)
        {
            PostTaskFinished(result);
        }

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PostProgressUpdate((BackgroundTaskProgressArgs)e.UserState);
        }

        protected void PostProgressUpdate(BackgroundTaskProgressArgs args)
        {
            if (ProgressUpdate != null)
                ProgressUpdate(this, args);
        }

        protected void PostTaskFinished(TFinished args)
        {
            if (TaskFinished != null)
                TaskFinished(this, args);
        }

        protected void RunWorker(TArgs args)
        {
            worker.RunWorkerAsync(args);
        }
    }
}