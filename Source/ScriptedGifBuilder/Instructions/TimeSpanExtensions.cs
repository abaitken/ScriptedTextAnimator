using System;

namespace ScriptedGifBuilder.Instructions
{
    internal static class TimeSpanExtensions
    {
        public static int ToGifDelay(this TimeSpan subject)
        {
            return (int) subject.TotalSeconds*100;
        }
    }
}