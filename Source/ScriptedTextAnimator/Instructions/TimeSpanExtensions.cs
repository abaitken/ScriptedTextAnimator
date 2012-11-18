using System;

namespace ScriptedTextAnimator.Instructions
{
    internal static class TimeSpanExtensions
    {
        public static int ToGifDelay(this TimeSpan subject)
        {
            return (int) subject.TotalSeconds*100;
        }
    }
}