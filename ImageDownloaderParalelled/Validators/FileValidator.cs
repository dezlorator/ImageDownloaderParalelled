using ImageDownloaderParalelled.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageDownloaderParalelled.Validators
{
    class FileValidator : IFileValidator
    {
        public bool IsFileExist(string pathToFile)
        {
            return File.Exists(pathToFile);
        }

        public bool IsFolderExist(string pathToFolder)
        {
            return Directory.Exists(pathToFolder);
        }
    }
}
