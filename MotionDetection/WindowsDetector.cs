using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionDetection
{
    using Detection;

    using Emgu.CV;
    using Emgu.CV.Structure;
    using Emgu.CV.UI;
    using Emgu.CV.Cuda;

    /// <summary>
    /// The windows detector.
    /// </summary>
    public partial class WindowsDetector : Form
    {
        /// <summary>
        /// Gets the capture.
        /// </summary>
        public new Capture Capture { get; }

        /// <summary>
        /// Gets the current image.
        /// </summary>
        public Image<Bgr, byte> CurrentImage { get; private set; }

        /// <summary>
        /// Gets the current image copy.
        /// </summary>
        public Image<Bgr, byte> CurrentImageCopy { get; private set; }

        /// <summary>
        /// Gets or sets the min hsv.
        /// </summary>
        public Hsv MinHsv { get; set; }

        /// <summary>
        /// Gets or sets the max hsv.
        /// </summary>
        public Hsv MaxHsv { get; set; }

        /// <summary>
        /// Gets or sets the mi hsv.
        /// </summary>
        public Hsv MiHsv { get; set; }

        /// <summary>
        /// Gets or sets the max ycc.
        /// </summary>
        public Ycc MaxYcc { get; set; }

        /// <summary>
        /// Gets or sets the min ycc.
        /// </summary>
        public Ycc MinYcc { get; set; }

        /// <summary>
        /// Gets or sets the frame width.
        /// </summary>
        public int FrameWidth { get; set; }

        /// <summary>
        /// Gets or sets the frame height.
        /// </summary>
        public int FrameHeight { get; set; }

        /// <summary>
        /// Gets or sets the box rect.
        /// </summary>
        public RotatedRect BoxRect { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsDetector"/> class.
        /// </summary>
        public WindowsDetector()
        {
            this.InitializeComponent();
            this.Capture = new Emgu.CV.Capture();
            this.Capture.QueryFrame();

            this.FrameHeight = this.Capture.Height;
            this.FrameWidth = this.Capture.Width;

            this.MinHsv = new Hsv(0, 45, 0);
            this.MaxHsv = new Hsv(20, 255, 255);
            this.MinYcc = new Ycc(0, 131, 80);
            this.MaxYcc = new Ycc(255, 185, 135);

            this.BoxRect = new RotatedRect();

            Application.Idle += new EventHandler(this.FrameCapture);
        }

        /// <summary>
        /// The frame capture.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void FrameCapture(object sender, EventArgs e)
        {
            this.CurrentImage = this.Capture.QueryFrame().ToImage<Bgr, byte>();

            if (this.CurrentImage != null)
            {
                this._imageBox.Image = this.CurrentImage;
            }
        }


    }
}
