﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloaderParalelled.Interfaces
{
    public interface IImageDownloaderService
    {
        bool DownloadImageAsync(string imageUrl, string destination);
    }
}
