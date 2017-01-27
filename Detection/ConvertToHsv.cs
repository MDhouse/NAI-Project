namespace Detection
{
    using System;

    using Emgu.CV;
    using Emgu.CV.Structure;

    /// <summary>
    /// The convert to hsv.
    /// </summary>
    public class ConvertToHsv : IConvertColor
    {
        /// <summary>
        /// Gets or sets the current hsv image.
        /// </summary>
        private Image<Hsv, byte> ConvertImageToHsvImage { get; set; }

        /// <summary>
        /// Gets or sets the skin image.
        /// </summary>
        private Image<Gray, byte> CurrentImage { get; set; }

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
            this.ConvertImageToHsvImage = image.Convert<Hsv, byte>();
            this.CurrentImage = this.ConvertImageToHsvImage.InRange((Hsv)minColor, (Hsv)maxColor);

            return this.CurrentImage;
        }
    }
}
