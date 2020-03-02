using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Interfaces
{
    public interface IFileValidator
    {
        bool IsFileExist(string pathToFile);
    }
}
