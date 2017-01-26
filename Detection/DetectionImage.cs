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
    using System;
    using System.Drawing;

    using Emgu.Util;
    using Emgu.CV;
    using Emgu.CV.Cuda;
    using Emgu.CV.CvEnum;
    using Emgu.CV.Structure;
    using Emgu.CV.Util;

    using static Emgu.CV.CvInvoke;

    class DetectionImage:IConvertColor
    {
        internal override Image<Gray, byte> DetectImage(Image<Bgr, byte> image, IColor minColor, IColor maxColor)
        {
            Image<Ycc, Byte> currentImage = image.Convert<Ycc, Byte>();
            Image<Gray, Byte> imageDetection = new Image<Gray, byte>(image.Width, image.Height);

            int y, cr, cb, l, x1, y1, value;

            var rows = image.Rows;
            var cols = image.Cols;

            Byte[,,] yccBytes = currentImage.Data;
            Byte[,,] imageDetectionBytes = imageDetection.Data;

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    y = yccBytes[i, j, 0];
                    cr = yccBytes[i, j, 1];
                    cb = yccBytes[i, j, 2];

                    cb -= 109;
                    cr -= 152;

                    x1 = (819 * cr - 614 * cb) / 32 + 51;
                    y1 = (819 * cr - 614 * cb) / 32 + 77;

                    x1 *= 41 / 1024;
                    y1 *= 73 / 1024;
                    value = x1 * x1 + y1 * y1;

                    if (y < 100)
                    {
                        imageDetectionBytes[i, j, 0] = (value < 700) ? (byte)255 : (byte)0;
                    }
                    else
                    {
                        imageDetectionBytes[i, j, 0] = (value < 850) ? (byte)255 : (byte)0;
                    }
                }


            }

            var mat = GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross, new Size(3, 3), new Point(1, 1));

            Erode(imageDetection, imageDetection, mat, new Point(6, 6),  1, BorderType.Wrap, MorphologyDefaultBorderValue);
            Dilate(imageDetection, imageDetection, mat, new Point(3, 3), 2, BorderType.Wrap, MorphologyDefaultBorderValue);

            return imageDetection;
        }
    }
}
