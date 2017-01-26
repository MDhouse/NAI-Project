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
    public abstract class IConvertColor
    {
        internal abstract Image<Gray, byte> DetectImage(Image<Bgr, byte> image, IColor minColor, IColor maxColor);
    }
}