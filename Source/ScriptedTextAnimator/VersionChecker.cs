using System;
using System.IO;
using System.Net;
using System.Windows;
using ScriptedTextAnimator.Background;
using ScriptedTextAnimator.Presentation.VersionCheck;

namespace ScriptedTextAnimator
{
    internal class VersionChecker : BasicBackgroundTask<EventArgs<Version>>
    {
        private readonly Window owner;
        private readonly bool alwaysShow;

        public VersionChecker(Window owner, bool alwaysShow)
        {
            this.owner = owner;
            this.alwaysShow = alwaysShow;
        }

        public bool IsWorking
        {
            get { return IsBusy; }
        }

        protected override EventArgs<Version> CreateExceptionResult(Exception exception)
        {
            return EventArgs<Version>.CreateExceptionResult(exception);
        }

        protected override EventArgs<Version> DoWorkImpl()
        {
            const string requestUriString = "http://temperedsoftware.co.uk/ScriptedTextAnimator/Version.txt";
            var webClient = (HttpWebRequest)WebRequest.Create(requestUriString);
            var response = webClient.GetResponse();

            string content;
            using(var reader = new StreamReader(response.GetResponseStream()))
            {
                content = reader.ReadToEnd();
            }

            var version = new Version(content);

            return EventArgs<Version>.CreateValidResult(version);
        }

        protected override void OnRunWorkerCompletedImpl(EventArgs<Version> result)
        {
            base.OnRunWorkerCompletedImpl(result);

            var hasException = result.Exception != null;
            if (!alwaysShow && hasException)
                return;

            var remoteVersion = hasException ? null : result.Value;

            var appInformationService = new ApplicationInformation();

            var localVersion = new Version(appInformationService.FileVersion);
            if (!hasException && !alwaysShow && localVersion == remoteVersion)
                return;

            var versionCheckWindow = new VersionCheckWindow();
            versionCheckWindow.ViewModel = new VersionCheckWindowViewModel(versionCheckWindow, localVersion, remoteVersion);
            versionCheckWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            versionCheckWindow.Owner = owner;
            versionCheckWindow.Show();
        }

        public void Check()
        {
            RunWorker();
        }
    }
}