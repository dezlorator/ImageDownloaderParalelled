using ImageDownloaderParalelled.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageDownloaderParalelled.Services
{
    class ImageDownloaderService : IImageDownloaderService
    {
        #region fields 
        private readonly HttpClient сlient;
        private readonly IFileValidator fileValidator;
        #endregion

        #region ctor
        public ImageDownloaderService(HttpClient client, IFileValidator fileValidator)
        {
            this.сlient = client;
            this.fileValidator = fileValidator;
        }
        #endregion

        public async Task<bool> DownloadImageAsync(string imageUrl, string destination)
        {
            if(fileValidator.IsFileExist(destination))
            {
                return false;
            }

            var responseResult = await сlient.GetAsync(imageUrl);
            using (var memStream = responseResult.Content.ReadAsStreamAsync().Result)
            {
                using (var fileStream = File.Create(destination))
                {
                    await memStream.CopyToAsync(fileStream);
                }
            }

            return true;
        }
    }
}
