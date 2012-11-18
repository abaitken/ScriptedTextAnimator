using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using ScriptedTextAnimator.Background;
using ScriptedTextAnimator.Instructions;

namespace ScriptedTextAnimator.Rendering
{
    internal class Animator : BackgroundTaskWithProgress<RenderFinishedArgs, RenderArgs>
    {
        public Animator()
            : base(true, true)
        {
        }

        private const int measureStage = 0;
        private const int expandInstructionsStage = 1;
        private const int renderStage = 2;
        private const int totalStages = renderStage + 1;

        protected override RenderFinishedArgs CreateExceptionResult(Exception exception)
        {
            return RenderFinishedArgs.Error(exception);
        }

        protected override RenderFinishedArgs DoWorkImpl(RenderArgs args)
        {
            if (args == null)
                throw new InvalidOperationException("Invalid render arguments");

            if (args.Script == null || args.Script.Length == 0)
                throw new InvalidOperationException("Script not defined");

            ReportProgress(0, new BackgroundTaskProgressArgs("Calculating", 0));

            var imageSize = Measure(args);
            var drawingInstructions = ExpandScript(args);
            var width = imageSize.Width;
            var height = imageSize.Height;

            var backgroundBrush = new SolidBrush(args.ProjectInstructions.BackgroundColor);

            var ms = new MemoryStream();
            var writer = new BinaryWriter(ms);
            var gifSignature = new[] {(byte) 'G', (byte) 'I', (byte) 'F', (byte) '8', (byte) '9', (byte) 'a'};

            writer.Write(gifSignature);

            var totalItems = drawingInstructions.Length;
            for (var i = 0; i < totalItems; i++)
            {
                var firstFrame = i == 0;
                var item = drawingInstructions[i];
                ReportProgress("Rendering", renderStage, i, totalItems);


                var surface = new Bitmap(width, height);
                using (var g = Graphics.FromImage(surface))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    g.FillRectangle(backgroundBrush, 0, 0, width, height);

                    item.Draw(g, imageSize, args.ProjectInstructions);
                }

                AddFrame(writer, surface, firstFrame, item.Delay);
            }

            writer.Write(CreateLoopBlock());

            writer.Write((byte)0x3B); //End file

            writer.Close();

            return RenderFinishedArgs.Success(ms.ToArray());
        }

        protected override void OnRunWorkerCompletedImpl(RenderFinishedArgs result)
        {
            switch (result.FinishState)
            {
                case RenderFinishState.Success:
                    PostProgressUpdate(new BackgroundTaskProgressArgs("Render finished", 100));
                    break;
                default:
                    PostProgressUpdate(new BackgroundTaskProgressArgs("Render error", 100));
                    break;
            }

            base.OnRunWorkerCompletedImpl(result);
        }

        public bool IsRendering
        {
            get { return IsBusy; }
        }

        public void StartRender(RenderArgs args)
        {
            if (IsBusy)
                throw new InvalidOperationException("Animator is currently busy");
            
            RunWorker(args);
        }

        private DrawingInstruction[] ExpandScript(RenderArgs args)
        {
            var results = new List<DrawingInstruction>
                          {
                              new DrawingInstruction
                              {
                                  Text = string.Empty,
                                  Delay = 1
                              }
                          };

            var totalItems = args.Script.Length;
            for (int i = 0; i < totalItems; i++)
            {
                ReportProgress("Processing", expandInstructionsStage, i, totalItems);
                var item = args.Script[i];

                var newInstructions = item.CreateInstructions(args.ProjectInstructions, args.Script, results);
                results.AddRange(newInstructions);
            }

            return results.ToArray();
        }

        private static void AddFrame(BinaryWriter writer, Bitmap surface, bool firstFrame, int delay)
        {
            var gif = new GifClass();
            gif.LoadGifPicture(surface);
            if (firstFrame)
                writer.Write(gif.m_ScreenDescriptor.ToArray());

            writer.Write(CreateGraphicControlExtensionBlock(delay));
            writer.Write(gif.m_ImageDescriptor.ToArray());
            writer.Write(gif.m_ColorTable.ToArray());
            writer.Write(gif.m_ImageData.ToArray());
        }

        private void ReportProgress(string message, double currentStage, double currentItem, double totalItems)
        {
            const int stageSlice = (100/totalStages);
            var overallStart = (stageSlice * currentStage);
            overallStart = overallStart == 0 ? 1 : overallStart;

            var stageProgress = ((stageSlice / totalItems) * currentItem);
            var progress = (int)(overallStart + stageProgress);
            
            progress = progress <= 100 ? progress : 100;
            var progressText = string.Format("Stage {3} of {4}: {0}. Item {1} of {2}", message, currentItem, totalItems, currentStage + 1, totalStages);
            ReportProgress(progress, new BackgroundTaskProgressArgs(progressText, progress));
        }

        private Size Measure(RenderArgs args)
        {
            var lineHeights = new List<int>
                              {
                                  0
                              };
            var lineWidths = new List<int>
                             {
                                 0
                             };
            var currentIndex = 0;

            var dummyImage = new Bitmap(1, 1);
            using (var g = Graphics.FromImage(dummyImage))
            {
                for (int i = 0; i < args.Script.Length; i++)
                {
                    ReportProgress("Measuring", measureStage, i, args.Script.Length);

                    var scriptInstruction = args.Script[i];

                    var measure = scriptInstruction.Measure(g, args.ProjectInstructions, args.Script);

                    if(measure.Size.HasValue)
                    {
                        var size = measure.Size.Value;
                        lineHeights[currentIndex] = Math.Max(lineHeights[currentIndex], size.Height);
                        lineWidths[currentIndex] += size.Width;
                    }

                    if (measure.OriginInstruction == OriginInstruction.NewRow)
                    {
                        lineHeights.Add(0);
                        lineWidths.Add(0);
                        currentIndex++;
                        continue;
                    }

                    if (measure.OriginInstruction == OriginInstruction.ResetOrigin)
                    {
                        currentIndex = 0;
                        continue;
                    }
                }
            }

            return new Size(lineWidths.Max(), lineHeights.Sum());
        }

        //private static string GetCursor(CursorTypes cursorType)
        //{
        //    switch (cursorType)
        //    {
        //        case CursorTypes.Underline:
        //            return "_";
        //        case CursorTypes.Block:
        //            return new string((char)219, 1);
        //        default:
        //            return string.Empty;
        //    }
        //}


        private static byte[] CreateGraphicControlExtensionBlock(int delay)
        {
            var result = new byte[8];

            // Split the delay into high- and lowbyte
            var d1 = (byte)(delay % 256);
            var d2 = (byte)(delay / 256);
            result[0] = 0x21; // Start ExtensionBlock
            result[1] = 0xF9; // GraphicControlExtension
            result[2] = 0x04; // Size of DataBlock (4)
            result[3] = d2;
            result[4] = d1;
            result[5] = 0x00;
            result[6] = 0x00;
            result[7] = 0x00;
            return result;
        }

        private static byte[] CreateLoopBlock()
        {
            return CreateLoopBlock(0);
        }

        private static byte[] CreateLoopBlock(int numberOfRepeatings)
        {
            var rep1 = (byte)(numberOfRepeatings % 256);
            var rep2 = (byte)(numberOfRepeatings / 256);
            var result = new byte[19];
            result[0] = 0x21; // Start ExtensionBlock
            result[1] = 0xFF; // ApplicationExtension
            result[2] = 0x0B; // Size of DataBlock (11) for NETSCAPE2.0)
            result[3] = (byte)'N';
            result[4] = (byte)'E';
            result[5] = (byte)'T';
            result[6] = (byte)'S';
            result[7] = (byte)'C';
            result[8] = (byte)'A';
            result[9] = (byte)'P';
            result[10] = (byte)'E';
            result[11] = (byte)'2';
            result[12] = (byte)'.';
            result[13] = (byte)'0';
            result[14] = 0x03; // Size of Loop Block
            result[15] = 0x01; // Loop Indicator
            result[16] = rep1; // Number of repetitions
            result[17] = rep2; // 0 for endless loop
            result[18] = 0x00;
            return result;
        }

    }
}