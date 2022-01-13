using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

using static System.Console;

string imagesFolder = Path.Combine(
  Environment.CurrentDirectory, "images");

WriteLine($"I will look for images in the following folder:\n{imagesFolder}");
WriteLine();

if (!Directory.Exists(imagesFolder))
{
  WriteLine();
  WriteLine("Folder does not exist!");
  return;
}

IEnumerable<string> images =
  Directory.EnumerateFiles(imagesFolder);

foreach (string imagePath in images)
{
  if (Path.GetFileNameWithoutExtension(imagePath).EndsWith("-thumbnail"))
  {
    WriteLine($"Skipping:\n  {imagePath}");
    WriteLine();
    continue; // this file has already been converted
  }

  string thumbnailPath = Path.Combine(
    Environment.CurrentDirectory, "images",
    Path.GetFileNameWithoutExtension(imagePath)
    + "-thumbnail" + Path.GetExtension(imagePath));

  using (Image image = Image.Load(imagePath))
  {
    WriteLine($"Converting:\n  {imagePath}");
    WriteLine($"To:\n  {thumbnailPath}");
    image.Mutate(x => x.Resize(image.Width / 10, image.Height / 10));
    image.Mutate(x => x.Grayscale());
    image.Save(thumbnailPath);
    WriteLine();
  }
}

WriteLine("Image processing complete. View the images folder.");
