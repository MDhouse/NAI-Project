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

    public partial class WindowsDetector : Form
    {
        public Mat CurrentImage;

        private Capture capture;
        //AdaptiveSkinDetector 

        public WindowsDetector()
        {
            this.InitializeComponent();
            this.capture = new Emgu.CV.Capture();
            this.capture.QueryFrame();

            var frameWidth = this.capture.Width;
            var frameHeight = this.capture.Height;

            var hsvMin = new Hsv(0, 45, 0);
            var hsvMax = new Hsv(20, 255, 255);
            var yccMin = new Ycc(0, 131, 80);
            var yccMax = new Ycc(255, 185, 135);

            Application.Idle += new EventHandler(FrameCapture);
        }

        public void FrameCapture(object sender, EventArgs e)
        {
            this.CurrentImage = this.capture.QueryFrame();

            if (this.CurrentImage != null)
            {
                this._imageToBox.Image = this.CurrentImage;
            }

        }
    }
}
