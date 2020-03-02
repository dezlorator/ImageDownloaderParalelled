using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Interfaces
{
    public interface IExcelPhotoURLGetterService
    {
        public IEnumerable<string> GetUrlFromFile(string pathToFile);
    }
}
