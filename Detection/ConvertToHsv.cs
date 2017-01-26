namespace Detection
{
    using System;

    using Emgu.CV;
    using Emgu.CV.Structure;

    public class ConvertToHsv : IConvertColor
    {
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
        internal override Image<Gray, byte> DetectImage(Image<Bgr, byte> image, IColor minColor, IColor maxColor)
        {
            var imageConvertToHsv = image.Convert<Hsv, byte>();
            var imageDetection = imageConvertToHsv.InRange((Hsv)minColor, (Hsv)maxColor);

            return imageDetection;
        }
    }
}
