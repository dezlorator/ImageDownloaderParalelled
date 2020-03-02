using ImageDownloaderParalelled.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Services
{
    class Controller : IController
    {
        #region
        private readonly IExcelPhotoURLGetterService excelPhotoURLGetter;
        private readonly IFileValidator fileValidator;
        #endregion

        public Controller(IExcelPhotoURLGetterService excelPhotoURLGetter, IFileValidator fileValidator)
        {
            this.excelPhotoURLGetter = excelPhotoURLGetter;
            this.fileValidator = fileValidator;
        }

        public IEnumerable<string> GetUrlFromFile(string pathToFile)
        {
            if(String.IsNullOrEmpty(pathToFile) || !fileValidator.IsFileExist(pathToFile))
            {
                return null;
            }

            return excelPhotoURLGetter.GetUrlFromFile(pathToFile);
        }


    }
}
