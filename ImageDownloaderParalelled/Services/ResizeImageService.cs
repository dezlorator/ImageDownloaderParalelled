using ImageDownloaderParalelled.Interfaces;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using SixLabors.Primitives;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageDownloaderParalelled.Services
{
    class ResizeImageService : IResizeImageService
    {
        #region
        private const int height = 300;
        private const int width = 300;
        #endregion

        public void ResizeImage(string pathToImage, string pathToSave)
        {
            using (Image<Rgba32> image = Image.Load<Rgba32>(pathToImage))
            {
                int line = image.Height < image.Width ? image.Height : image.Width;
                int y = image.Height < image.Width ? 0 : (image.Height - line) / 2;
                int x = image.Height < image.Width ? (image.Width - line) / 2 : 0;
                image.Mutate(tx => tx.Crop(new Rectangle(x, y, line, line)));
                image.Mutate(ctx => ctx.Resize(height, width));
                image.Save(pathToSave);
            }
        }
    }
}
