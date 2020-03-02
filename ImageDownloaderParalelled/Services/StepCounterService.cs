using ImageDownloaderParalelled.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Services
{
    class StepCounterService : IStepCounterService
    {
        #region fields 
        private const int step = 20;
        #endregion

        public int GetStep(int numberOfElementsInContainer)
        {
            if(numberOfElementsInContainer < step)
            {
                return numberOfElementsInContainer;
            }
            else
            {
                return step;
            }
        }
    }
}
