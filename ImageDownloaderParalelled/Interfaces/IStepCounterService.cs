using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Interfaces
{
    public interface IStepCounterService
    {
        int GetStep(int numberOfElementsInContainer);
    }
}
