using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using static System.Console;

namespace WorkingWithImages
{
  class Program
  {
    static void Main(string[] args)
    {
      string imagesFolder = Path.Combine(
        Environment.CurrentDirectory, "images");

      IEnumerable<string> images =
        Directory.EnumerateFiles(imagesFolder);

      foreach (string imagePath in images)
      {
        string thumbnailPath = Path.Combine(
          Environment.CurrentDirectory, "images",
          Path.GetFileNameWithoutExtension(imagePath)
          + "-thumbnail" + Path.GetExtension(imagePath));

        using (Image image = Image.Load(imagePath))
        {
          image.Mutate(x => x.Resize(image.Width / 10, image.Height / 10));
          image.Mutate(x => x.Grayscale());
          image.Save(thumbnailPath);
        }
      }
      WriteLine("Image processing complete. View the images folder.");
    }
  }
}