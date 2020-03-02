using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Interfaces
{
    public interface IConvertor<ConvertFrom, ConvertTo>
    {
        ConvertTo Convert(ConvertFrom value);
    }
}
