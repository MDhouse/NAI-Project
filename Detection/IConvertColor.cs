// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConvertColor.cs" company="">
// </copyright>
// <summary>
//   Defines the IConvertColor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Detection
{
    using Emgu.CV;
    using Emgu.CV.Structure;

    /// <summary>
    /// The convert color.
    /// </summary>
    public interface IConvertColor
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
        Image<Gray, byte> DetectImage(Image<Bgr, byte> image, IColor minColor, IColor maxColor);
    }
}