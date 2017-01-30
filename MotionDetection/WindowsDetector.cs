using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionDetection
{
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.IO;

    using Detection;

    using Emgu.CV;
    using Emgu.CV.Structure;
    using Emgu.CV.UI;
    using Emgu.CV.CvEnum;
    using Emgu.CV.Util;

    using static Emgu.CV.CvInvoke;

    using Capture = Emgu.CV.Capture;
    using Point = System.Drawing.Point;
    using RotatedRect = Emgu.CV.Structure.RotatedRect;
    using Stream = Emgu.CV.Cuda.Stream;

    /// <summary>
    /// The windows detector.
    /// </summary>
    public partial class WindowsDetector : Form
    {
        /// <summary>
        /// Gets or sets the convert color.
        /// </summary>
        private IConvertColor ConvertColor { get; set; }

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
        /// Gets or sets the number of fingers.
        /// </summary>
        public int NumberOfFingers { get; set; }

        /// <summary>
        /// Gets or sets the box rect.
        /// </summary>
        public RotatedRect BoxRect { get; set; }

        /// <summary>
        /// Gets or sets the defects of point.
        /// </summary>
        public VectorOfPoint DefectsOfPoint { get; set; }

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
            this.MaxHsv = new Hsv(60, 255, 255);
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
        /// <param name="e">6
        /// The e.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1503:CurlyBracketsMustNotBeOmitted",
             Justification = "Reviewed. Suppression is OK here.")]
        private void FrameCapture(object sender, EventArgs e)
        {
            this.CurrentImage = this.Capture.QueryFrame().ToImage<Bgr, byte>();

            if (this.CurrentImage == null) return;
            this.CurrentImageCopy = this.CurrentImage.Copy();

            this._imageBox.Image = this.CurrentImage;

            this.ConvertColor = new ConvertToHsv();
            //var returnImage = this.ConvertColor.DetectImage(this.CurrentImageCopy, this.MinYcc, this.MaxYcc);
            var returnImage = this.ConvertColor.DetectImage(this.CurrentImageCopy, this.MinHsv, this.MaxHsv);

            //this.ShellingCountour(this.CurrentImageCopy);
            this.ShellingCountour(returnImage);

            this._imageBox.Image = this.CurrentImage;
            this._imageToBox.Image = returnImage;
        }

        /// <summary>
        /// The shelling countour.
        /// </summary>
        /// <param name="convertImage">
        /// The convert image.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public void ShellingCountour(Image<Gray, byte> convertImage)
        {
            var copyImage = convertImage.Convert<Gray, byte>();

            var largeContour = 0;
            double largestArea = 0;
            VectorOfPoint largeContourOfPoint = null;
            VectorOfPoint hullPoint = new VectorOfPoint();
            RotatedRect box = new RotatedRect();


            using (var contoursOfPoint = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(
                    copyImage,
                    contoursOfPoint,
                    null,
                    RetrType.External,
                    ChainApproxMethod.ChainApproxTc89Kcos);

                var count = contoursOfPoint.Size;

                for (var i = 0; i < count; i++)
                {
                    var approx = ContourArea(contoursOfPoint[i], false);

                    if (approx > largestArea)
                    {
                        largestArea = approx;
                        largeContourOfPoint = new VectorOfPoint(contoursOfPoint[i].ToArray());
                        largeContour = i;
                    }
                }

                if (largeContourOfPoint != null)
                {
                    var currentContour = new VectorOfPoint();

                    ApproxPolyDP(
                        largeContourOfPoint,
                        currentContour,
                        ArcLength(largeContourOfPoint, true) * 0.85,
                        true);

                    ContourArea(currentContour, false);
                    DrawContours(this.CurrentImage, contoursOfPoint, largeContour, new MCvScalar(0, 255, 0));


                    ConvexHull(largeContourOfPoint, hullPoint, false, true);
                    box = MinAreaRect(largeContourOfPoint);
                    PointF[] points = box.GetVertices();

                    Point[] ps = new Point[points.Length];
                    for (var j = 0; j < points.Length; j++)
                    {
                        ps[j] = new Point((int)points[j].X, (int)points[j].Y);
                    }

                    this.CurrentImage.DrawPolyline(hullPoint.ToArray(), true, new Bgr(200, 125, 75), 2);
                    this.CurrentImage.Draw(new CircleF(new PointF(box.Center.X, box.Center.Y), 3), new Bgr(200, 125, 75), 2);

                    ConvexityDefects(largeContourOfPoint, hullPoint, this.DefectsOfPoint);
                }
            }
        }

        private void FindersNumber()
        {
        }

        private void DrawItemEventArgs()
        {
            //for(var i = 0; i )
        }

    }
}