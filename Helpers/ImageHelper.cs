using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Helpers
{
  public static class ImageHelper
  {
    const int Quality = 75;
    public static void ResizeAndSave(Stream fileStream, string outputPath, int size)
    {
      using (var image = new Bitmap(System.Drawing.Image.FromStream(fileStream)))
      {
        int width, height;
        if (image.Width > image.Height)
        {
          width = size;
          height = image.Height * size / image.Width;
        }
        else
        {
          width = image.Width * size / image.Height;
          height = size;
        }

        using (var resized = new Bitmap(width, height))
        {

          using (var graphics = Graphics.FromImage(resized))
          {
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.DrawImage(image, 0, 0, width, height);
            using (var output = File.Open(outputPath, FileMode.Create))
            {
              var qualityParamId = Encoder.Quality;
              var encoderParameters = new EncoderParameters(1);
              encoderParameters.Param[0] = new EncoderParameter(qualityParamId, Quality);
              var codec = ImageCodecInfo.GetImageDecoders()
                  .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
              resized.Save(output, codec, encoderParameters);
            }
            //resized.Save(outputPath);
          }
        }
      }
    }

    public static void SaveOriginal(Stream fileStream, string outputPath)
    {
      using (var image = new Bitmap(System.Drawing.Image.FromStream(fileStream)))
      {
        using (var graphics = Graphics.FromImage(image))
        {
          graphics.CompositingQuality = CompositingQuality.HighSpeed;
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.CompositingMode = CompositingMode.SourceCopy;
          graphics.DrawImage(image, 0, 0, image.Width, image.Height);
          image.Save(outputPath);
        }
      }
    }
  }
}
