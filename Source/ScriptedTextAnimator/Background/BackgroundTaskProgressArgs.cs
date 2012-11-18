using System;

namespace ScriptedTextAnimator.Background
{
    [Serializable]
    internal class BackgroundTaskProgressArgs : EventArgs
    {
        private readonly string currentStep;
        private readonly int percentageComplete;

        public BackgroundTaskProgressArgs(string currentStep, int percentageComplete)
        {
            this.currentStep = currentStep;
            this.percentageComplete = percentageComplete;
        }

        public int PercentageComplete
        {
            get { return percentageComplete; }
        }

        public string CurrentStep
        {
            get { return currentStep; }
        }
    }
}