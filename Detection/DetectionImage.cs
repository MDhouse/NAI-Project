// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetectionImage.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DetectionImage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Detection
{
    using System.Drawing;

    using Emgu.CV;
    using Emgu.CV.CvEnum;
    using Emgu.CV.Structure;

    public class DetectionImage : IConvertColor
    {
        private byte[,,] yccBytes;
        private byte[,,] grayBytes;

        /// <summary>
        /// Gets or sets the convert image to ycc image.
        /// </summary>
        private Image<Ycc, byte> ConvertImageToYccImage { get; set; }

        /// <summary>
        /// Gets or sets the current detection image.
        /// </summary>
        private Image<Gray, byte> CurrentDetectionImage { get; set; }

        /// <summary>
        /// Gets or sets the structur mat.
        /// </summary>
        private Mat StructurMat { get; set; }

        public Image<Gray, byte> DetectImage(Image<Bgr, byte> image, IColor minColor, IColor maxColor)
        {
            this.ConvertImageToYccImage = image.Convert<Ycc, byte>();
            this.CurrentDetectionImage = new Image<Gray, byte>(image.Width, image.Height);

            this.Rows = image.Rows;
            this.Cols = image.Cols;
                
            this.yccBytes = this.ConvertImageToYccImage.Data;
            this.grayBytes = this.CurrentDetectionImage.Data;

            for (var i = 0; i < this.Rows; i++)
            {
                for (var j = 0; j < this.Cols; j++)
                {
                    int first = this.yccBytes[i, j, 0];
                    int second = this.yccBytes[i, j, 1];
                    int third = this.yccBytes[i, j, 2];

                    third = third - 109;
                    second = second - 152;

                    int firstHalf = (819 * second - 614 * third) / 32 + 51;
                    int secondHalf = (819 * second - 614 * third) / 32 + 77;

                    firstHalf *= 41 / 1024;
                    secondHalf *= 73 / 1024;

                    int result = firstHalf * firstHalf + secondHalf * secondHalf;

                    if (first < 100)
                    {
                        this.grayBytes[i, j, 0] = (result < 700) ? (byte)255 : (byte)0;
                    }
                    else
                    {
                        this.grayBytes[i, j, 0] = (result < 850) ? (byte)255 : (byte)0;
                    }
                }
            }

            this.FirstMat = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(12, 12), new Point(6, 6));
            this.SecondMat = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(6, 6), new Point(3, 3));

            CvInvoke.Erode(this.CurrentDetectionImage, this.CurrentDetectionImage, this.FirstMat, new Point(2, 2), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);
            CvInvoke.Dilate(this.CurrentDetectionImage, this.CurrentDetectionImage, this.SecondMat, new Point(2, 2), 2, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);

            return this.CurrentDetectionImage;
        }

        /// <summary>
        /// Gets or sets the second mat.
        /// </summary>
        private Mat SecondMat { get; set; }

        /// <summary>
        /// Gets or sets the first mat.
        /// </summary>
        private Mat FirstMat { get; set; }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        private int Cols { get; set; }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        private int Rows { get; set; }
    }
}
