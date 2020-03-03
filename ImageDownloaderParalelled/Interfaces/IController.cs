using ImageDownloaderParalelled.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloaderParalelled.Interfaces
{
    public interface IController
    {
        IEnumerable<string> GetUrlFromFile(string pathToFile);
        Task<bool> DownloadImage(string imageUrl, string destination);
        string CreatePathToImageBeforeResizing(string imageUrl);
        string CreatePathToImageAfterResizing(string imageName);
        bool ResizePhoto(string pathToImage, string pathToSave);
        Task PutIntoDatabase(ImageWithUrl imageWithUrl, string imagePath);
    }
}
