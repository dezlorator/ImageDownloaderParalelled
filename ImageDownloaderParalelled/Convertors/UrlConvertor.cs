using ImageDownloaderParalelled.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Convertors
{
    class UrlConvertor : IConvertor<string, Uri>
    {
        public Uri Convert(string value)
        {
            return new Uri(value);
        }
    }
}
