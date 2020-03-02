using ClosedXML.Excel;
using ImageDownloaderParalelled.Interfaces;
using ImageDownloaderParalelled.Modules;
using ImageDownloaderParalelled.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageDownloaderParalelled
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToFile = System.IO.Directory.GetCurrentDirectory() + @"\Excel\99.xlsx";

            var kernel = new StandardKernel(new ModelBindingModule());
            var mainWork = kernel.Get<Controller>();
            IEnumerable<string> urlContainer;

            do
            {
                urlContainer = mainWork.GetUrlFromFile(pathToFile);
            }
            while (urlContainer != null || urlContainer.Any());

            Console.ReadKey();
        }
    }
}
