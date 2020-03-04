using ImageDownloaderParalelled.Interfaces;
using ImageDownloaderParalelled.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloaderParalelled.Services
{
    class Controller : IController
    {
        #region
        private readonly IExcelPhotoURLGetterService excelPhotoURLGetter;
        private readonly IFileValidator fileValidator;
        private readonly IPathCreator pathCreator;
        private readonly IImageDownloaderService imageDownloaderService;
        private readonly IResizeImageService resizeImageService;
        private readonly IMongoDBService mongoDBService;
        #endregion

        public Controller(IExcelPhotoURLGetterService excelPhotoURLGetter, IFileValidator fileValidator,
            IPathCreator pathCreator, IImageDownloaderService imageDownloaderService, IResizeImageService resizeImageService,
            IMongoDBService mongoDBService)
        {
            this.excelPhotoURLGetter = excelPhotoURLGetter;
            this.fileValidator = fileValidator;
            this.pathCreator = pathCreator;
            this.imageDownloaderService = imageDownloaderService;
            this.resizeImageService = resizeImageService;
            this.mongoDBService = mongoDBService;
        }

        public string CreatePathToImageAfterResizing(string imageName)
        {
            if (String.IsNullOrEmpty(imageName))
            {
                return null;
            }

            return pathCreator.CreatePathForImageAfterResizing(imageName);
        }

        public string CreatePathToImageBeforeResizing(string imageUrl)
        {
            if(String.IsNullOrEmpty(imageUrl))
            {
                return null;
            }

            return pathCreator.CreatePathForImageBeforeResizing(imageUrl);
        }

        public async Task<bool> DownloadImage(string imageUrl, string destination)
        {
            if(String.IsNullOrEmpty(imageUrl))
            {
                return false;
            }

            await imageDownloaderService.DownloadImageAsync(imageUrl, destination);

            return true;
        }

        public IEnumerable<string> GetUrlFromFile(string pathToFile)
        {
            if(String.IsNullOrEmpty(pathToFile) || !fileValidator.IsFileExist(pathToFile))
            {
                return null;
            }

            return excelPhotoURLGetter.GetUrlFromFile(pathToFile);
        }

        public bool ResizePhoto(string pathToImage, string pathToSave)
        {
            if(String.IsNullOrEmpty(pathToImage) || !fileValidator.IsFileExist(pathToImage))
            {
                return false;
            }

            if (String.IsNullOrEmpty(pathToSave) || fileValidator.IsFileExist(pathToSave))
            {
                return false;
            }

            resizeImageService.ResizeImage(pathToImage, pathToSave);

            return true;
        }

        public async Task PutIntoDatabase(ImageWithUrl imageWithUrl, string imagePath)
        {
            await mongoDBService.Create(imageWithUrl, imagePath);
        }
    }
}
