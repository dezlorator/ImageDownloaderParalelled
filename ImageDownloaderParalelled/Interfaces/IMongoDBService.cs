using ImageDownloaderParalelled.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloaderParalelled.Interfaces
{
    public interface IMongoDBService
    {
        Task Create(ImageWithUrl imageWithUrl, string imagePath);
    }
}
