using System.Drawing;
using System.Drawing.Drawing2D;
using ScriptedTextAnimator.Instructions;

namespace ScriptedTextAnimator.Rendering
{
    internal struct DrawingInstruction
    {
        public string Text { get; set; }
        public bool Cursor { get; set; }
        public int Delay { get; set; }

        public void Draw(Graphics g, Size imageSize, ProjectInstructions projectInstructions)
        {
            if (string.IsNullOrEmpty(Text) && !Cursor)
                return;

            var font = RenderingHelper.CreateFont(g, projectInstructions.FontFamily, projectInstructions.FontStyle,
                                                  projectInstructions.FontSize);

            var drawLocation = new PointF(0, 0);

            var text = string.Concat(Text, Cursor ? "_" : string.Empty);

            if (projectInstructions.DrawFromBottom)
            {
                var size = g.MeasureString(string.Concat(text, "EMPY LINES HAVE NO HEIGHT"), font);

                drawLocation = new PointF(0, imageSize.Height - size.Height);
            }

            if (projectInstructions.Outline)
            {
                using (var path = new GraphicsPath())
                {
                    path.AddString(text, font.FontFamily, (int)font.Style, font.Size, drawLocation,
                                   StringFormat.GenericDefault);

                    var p = new Pen(projectInstructions.OutlineColor, projectInstructions.OutlineWidth);
                    g.DrawPath(p, path);
                    g.FillPath(new SolidBrush(projectInstructions.FontColor), path);
                }
            }
            else
            {
                g.DrawString(text, font, new SolidBrush(projectInstructions.FontColor), drawLocation);
            }

            var pen = new Pen(projectInstructions.BackgroundColor, projectInstructions.ScanlineWidth);
            var increment = projectInstructions.ScanlineGap + projectInstructions.ScanlineWidth;
            if (projectInstructions.HorizontalScanLines)
            {
                for (var lineY = 0; lineY < imageSize.Height; lineY += increment)
                {
                    g.DrawLine(pen, 0, lineY, imageSize.Width, lineY);
                }
            }
            if (projectInstructions.VericalScanLines)
            {
                for (var lineX = 0; lineX < imageSize.Width; lineX += increment)
                {
                    g.DrawLine(pen, lineX, 0, lineX, imageSize.Height);
                }
            }
        }

        public DrawingInstruction Copy()
        {
            var result = new DrawingInstruction
                         {
                             Cursor = Cursor,
                             Text = Text,
                             Delay = Delay
                         };

            return result;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}