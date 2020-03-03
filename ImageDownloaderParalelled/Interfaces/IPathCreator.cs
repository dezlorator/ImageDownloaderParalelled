using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Interfaces
{
    public interface IPathCreator
    {
        string CreatePathForImageBeforeResizing(string imageUrl);
        string CreatePathForImageAfterResizing(string fileName);
    }
}
