using ImageDownloaderParalelled.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloaderParalelled.Interfaces
{
    interface IMongoDbRepository
    {
        Task<ImageWithUrl> GetProduct(string id);
        Task<string> Create(ImageWithUrl p);
        Task Update(ImageWithUrl p);
        Task Remove(string id);
        Task<byte[]> GetImage(string id);
        Task StoreImage(string id, Stream imageStream, string imageName);
    }
}
