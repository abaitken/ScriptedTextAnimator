using System;

namespace ScriptedTextAnimator.Helpers
{
    internal static class TimeSpanExtensions
    {
        public static int ToGifDelay(this TimeSpan subject)
        {
            return (int) subject.TotalSeconds*100;
        }
    }
}