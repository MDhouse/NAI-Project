// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConvertToYcc.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ConvertToYcc type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Detection
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    using Emgu.CV;
    using Emgu.CV.CvEnum;
    using Emgu.CV.Structure;

    using static Emgu.CV.CvInvoke;

    /// <summary>
    /// The convert to ycc.
    /// </summary>
    public class ConvertToYcc : IConvertColor
    {
        /// <summary>   
        /// Gets or sets the convert image to ycc image.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private Image<Ycc, byte> ConvertImageToYccImage { get; set; }

        /// <summary>
        /// Gets or sets the current image.
        /// </summary>
        private Image<Gray, byte> CurrentImage { get; set; }

        /// <summary>
        /// Gets or sets the create mat.
        /// </summary>
        private Mat FirstMat { get; set; }

        /// <summary>
        /// Gets or sets the mat second mat.
        /// </summary>
        private Mat SecondMat { get; set; }

        /// <summary>
        /// The detect image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="minColor">
        /// The min color.
        /// </param>
        /// <param name="maxColor">
        /// The max color.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public Image<Gray, byte> DetectImage(Image<Bgr, byte> image, IColor minColor, IColor maxColor)
        {
            this.ConvertImageToYccImage = image.Convert<Ycc, byte>();
            
            this.CurrentImage = new Image<Gray, byte>(image.Width, image.Height);
            this.CurrentImage = this.ConvertImageToYccImage.InRange((Ycc)minColor, (Ycc)maxColor);

            this.FirstMat = GetStructuringElement(ElementShape.Rectangle, new Size(12, 12), new Point(6, 6));
            this.SecondMat = GetStructuringElement(ElementShape.Rectangle, new Size(6, 6), new Point(3, 3));

            Erode(this.CurrentImage, this.CurrentImage, this.FirstMat, new Point(2, 2), 1, BorderType.Constant, MorphologyDefaultBorderValue);
            Dilate(this.CurrentImage, this.CurrentImage, this.SecondMat, new Point(2, 2), 2, BorderType.Constant, MorphologyDefaultBorderValue);

            return this.CurrentImage;
        }
    }
}
