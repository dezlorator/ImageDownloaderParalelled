using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Interfaces
{
    public interface IResizeImageService
    {
        void ResizeImage(string pathToImage, string pathToSave);
    }
}
