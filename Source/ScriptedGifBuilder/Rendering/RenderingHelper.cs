using System.Drawing;

namespace ScriptedGifBuilder.Rendering
{
    static class RenderingHelper
    {
        public static Font CreateFont(Graphics g, string fontFamily, FontStyle fontStyle, int fontSize)
        {
            var emSize = (g.DpiY / 72) * fontSize;
            var font = new Font(fontFamily, emSize,
                                fontStyle, GraphicsUnit.Pixel);

            return font;
        }
    }
}
