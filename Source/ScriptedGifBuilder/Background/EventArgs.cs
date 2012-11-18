using System;
using ScriptedGifBuilder.Rendering;

namespace ScriptedGifBuilder.Background
{
    class EventArgs<T> : BackgroundTaskFinishedArgs
    {
        private readonly T value;

        private EventArgs(T value, Exception exception)
            : base(exception)
        {
            this.value = value;
        }

        public T Value
        {
            get { return value; }
        }

        public static EventArgs<T> CreateExceptionResult(Exception ex)
        {
            return new EventArgs<T>(default(T), ex);
        }

        public static EventArgs<T> CreateValidResult(T value)
        {
            return new EventArgs<T>(value, null);
        }
    }
}