using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Interfaces
{
    public interface IController
    {
        IEnumerable<string> GetUrlFromFile(string pathToFile);
    }
}
