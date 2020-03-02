using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Interfaces
{
    public interface IPathCreator
    {
        string CreatePathForImage(string imageName);
    }
}
