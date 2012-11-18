using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using ScriptedGifBuilder.Rendering;

namespace ScriptedGifBuilder.Instructions
{
    public class ProjectInstructions : ScriptedInstruction
    {
        [ScriptedProperty("Background Color", typeof (BlackDefaultValueStrategy))]
        [XmlIgnore]
        public Color BackgroundColor { get; set; }

        public int BackgroundARGB
        {
            get { return BackgroundColor.ToArgb(); }
            set { BackgroundColor = Color.FromArgb(value); }
        }

        [ScriptedProperty("Draw from bottom", false)]
        public bool DrawFromBottom { get; set; }

        [ScriptedProperty("Font Family", typeof (FontValueStrategy))]
        public string FontFamily { get; set; }

        [ScriptedProperty("Font Style", FontStyle.Bold)]
        public FontStyle FontStyle { get; set; }

        [ScriptedProperty("Font Color", typeof (LimeGreenDefaultValueStrategy))]
        public Color FontColor { get; set; }

        public int FontARGB
        {
            get { return FontColor.ToArgb(); }
            set { FontColor = Color.FromArgb(value); }
        }

        [ScriptedProperty("Font Size", 36, 1, 1000)]
        public int FontSize { get; set; }

        [ScriptedProperty("Draw Cursor", true)]
        public bool DrawCursor { get; set; }

        [ScriptedProperty("Outline", false)]
        public bool Outline { get; set; }

        [ScriptedProperty("Outline Color", typeof (DarkGreenDefaultValueStrategy))]
        public Color OutlineColor { get; set; }

        public int OutlineARGB
        {
            get { return OutlineColor.ToArgb(); }
            set { OutlineColor = Color.FromArgb(value); }
        }

        [ScriptedProperty("Outline Width", 0, 0, 100)]
        public int OutlineWidth { get; set; }

        [ScriptedProperty("Horizontal Scanlines", false)]
        public bool HorizontalScanLines { get; set; }

        [ScriptedProperty("Vertical Scanlines", false)]
        public bool VericalScanLines { get; set; }

        [ScriptedProperty("Scanline Width", 1, 1, 100)]
        public int ScanlineWidth { get; set; }

        [ScriptedProperty("Scanline Gap", 1, 1, 100)]
        public int ScanlineGap { get; set; }


        public override string DisplayText
        {
            get { return "Project Instructions"; }
        }

        internal override Measure Measure(Graphics g, ProjectInstructions projectInstructions, ScriptedInstruction[] script)
        {
            throw new InvalidOperationException();
        }

        internal override IList<DrawingInstruction> CreateInstructions(ProjectInstructions projectInstructions, IList<ScriptedInstruction> script, IList<DrawingInstruction> currentInstructions)
        {
            throw new NotSupportedException();
        }
    }
}