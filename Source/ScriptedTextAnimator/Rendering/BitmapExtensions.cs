using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ScriptedTextAnimator.Rendering
{
    internal static class BitmapExtensions
    {
        public static BitmapSource ToBitmapSource(this Bitmap source)
        {
            BitmapSource bitSrc;

            var hBitmap = source.GetHbitmap();

            try
            {
                bitSrc = Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Win32Exception)
            {
                bitSrc = null;
            }
            finally
            {
                NativeMethods.DeleteObject(hBitmap);
            }

            return bitSrc;
        }

        public static void SaveAsGIF(this Bitmap source, Stream destination, int quality)
        {
            var encoders = ImageCodecInfo.GetImageEncoders().ToDictionary(k => k.MimeType.ToLower(), v => v);
            var gifEncoder = encoders["image/gif"];

            var p = new[]
                    {
                        new EncoderParameter(Encoder.Quality, 100)
                    };
            var encoderParameters = new EncoderParameters
            {
                Param = p
            };
            source.Save(destination, gifEncoder, encoderParameters);
        }
    }
}