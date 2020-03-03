using ImageDownloaderParalelled.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloaderParalelled.Services
{
    class ImageDownloaderService : IImageDownloaderService
    {
        #region fields 
        private readonly WebClient webClient;
        private readonly IFileValidator fileValidator;
        private readonly IConvertor<string, Uri> urlConvertor;
        #endregion

        #region ctor
        public ImageDownloaderService(WebClient webClient, IFileValidator fileValidator, IConvertor<string, Uri> urlConvertor)
        {
            this.webClient = webClient;
            this.fileValidator = fileValidator;
            this.urlConvertor = urlConvertor;
        }
        #endregion

        public bool DownloadImageAsync(string imageUrl, string destination)
        {
            if(fileValidator.IsFileExist(destination))
            {
                return false;
            }

            webClient.DownloadFile(urlConvertor.Convert(imageUrl), destination);

            return true;
        }
    }
}
