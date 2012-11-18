using System;

namespace ScriptedGifBuilder.Rendering
{
    [Serializable]
    internal abstract class BackgroundTaskFinishedArgs : EventArgs
    {
        private readonly Exception exception;

        protected BackgroundTaskFinishedArgs(Exception exception)
        {
            this.exception = exception;
        }

        public Exception Exception
        {
            get { return exception; }
        }
    }

    [Serializable]
    internal class RenderFinishedArgs : BackgroundTaskFinishedArgs
    {
        private readonly RenderFinishState finishState;
        private readonly byte[] image;

        private RenderFinishedArgs(RenderFinishState finishState, byte[] image, Exception exception)
            : base(exception)
        {
            this.finishState = finishState;
            this.image = image;
        }

        public byte[] Image
        {
            get { return image; }
        }

        public RenderFinishState FinishState
        {
            get { return finishState; }
        }

        public static RenderFinishedArgs Success(byte[] image)
        {
            return new RenderFinishedArgs(RenderFinishState.Success, image, null);
        }

        public static RenderFinishedArgs Error(Exception ex)
        {
            return new RenderFinishedArgs(RenderFinishState.Error, null, ex);
        }
    }
}