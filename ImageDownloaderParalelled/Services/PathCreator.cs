using ImageDownloaderParalelled.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Services
{
    class PathCreator : IPathCreator
    {
        #region property
        private string CurrentDirectory
        {
            get
            {
                return System.IO.Directory.GetCurrentDirectory();
            }
        }

        private List<string> fileExtension = new List<string>
        {
            "jpg", "png", "bmp", "psd", "tga", "tiff"
        };
        #endregion

        #region fields
        private static string imagesBeforeResizingFolder = @"Images\ImagesBeforeResizing";
        private static string imagesAfterResizingFolder = @"Images\ImagesAfterResizing";
        #endregion

        public string CreatePathForImageBeforeResizing(string imageUrl)
        {
            var splittedName = imageUrl.Split('/');

            var path = CurrentDirectory + @"\" + imagesBeforeResizingFolder + @"\" + CreateFileName(splittedName[splittedName.Length - 1]);

            return path;
        }

        private string CreateFileName(string imageName)
        {
            if (imageName.Split('.').Length == 0 || !fileExtension.Contains(imageName.Split('.')[1]))
            {
                imageName += ".jpg";
            }

            return imageName;
        }

        public string CreatePathForImageAfterResizing(string fileName)
        {
            return CurrentDirectory + @"\" + imagesAfterResizingFolder + @"\" + fileName;
        }
    }
}
