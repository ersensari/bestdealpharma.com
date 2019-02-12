using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace bestdealpharma.com.Helpers
{
  public static class UploadHelper
  {

    /// <summary>
    /// upload image and documents
    /// </summary>
    /// <param name="file"></param>
    /// <param name="imageSize"></param>
    /// <param name="fileSuffix"></param>
    /// <param name="webRootPath"></param>
    /// <param name="folders"></param>
    /// <returns></returns>
    /// <exception cref="FileUploadException"></exception>
    public static string UploadSingle(IFormFile file, int imageSize, string fileSuffix, string webRootPath,
      params string[] folders)
    {
      List<string> _folders = new List<string>();
      _folders.Add(webRootPath);
      _folders.AddRange(folders);

      string absoluteFilePath = Path.Combine(_folders.ToArray());
      if (!Directory.Exists(absoluteFilePath))
      {
        Directory.CreateDirectory(absoluteFilePath);
      }

      if (file.Length > 0)
      {
        try
        {
          Guid _guid = Guid.NewGuid();
          absoluteFilePath = Path.Combine(absoluteFilePath,
            string.Format("{0}{1}{2}", _guid, fileSuffix, Path.GetExtension(file.FileName)));
          string filePath = string.Format("{0}/{1}{2}{3}", Path.Combine(folders), _guid, fileSuffix,
            Path.GetExtension(file.FileName));
          if (file.ContentType.Contains("image"))
          {
            if (imageSize > 0)
            {
              ImageHelper.ResizeAndSave(file.OpenReadStream(), absoluteFilePath, imageSize);
            }
            else
            {
              ImageHelper.SaveOriginal(file.OpenReadStream(), absoluteFilePath);
            }
          }
          else
          {
            using (var stream = new FileStream(absoluteFilePath, FileMode.Create))
            {
              file.CopyTo(stream);
            }
          }

          return filePath;
        }
        catch (Exception ex)
        {
          throw new FileUploadException("An error occorupted while create file...<br/>" + ex.ToString());
        }
      }
      else
      {
        throw new FileUploadException("Please select a file...");
      }
    }
  }

  [Serializable]
  public class FileUploadException : Exception
  {
    public FileUploadException()
    {
    }

    public FileUploadException(string message) : base(message)
    {
    }

    public FileUploadException(string message, Exception inner) : base(message, inner)
    {
    }

    protected FileUploadException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}
