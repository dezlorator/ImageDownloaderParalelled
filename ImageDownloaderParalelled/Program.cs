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
            string pathToFile = System.IO.Directory.GetCurrentDirectory() + @"\Excel\Nature.xlsx";

            var kernel = new StandardKernel(new ModelBindingModule());
            var controller = kernel.Get<IController>();
            IEnumerable<string> urlContainer;
            int counter = 1;

            do
            {
                urlContainer = controller.GetUrlFromFile(pathToFile);
                Console.WriteLine($"{urlContainer.Count()} was downloaded from the file");
                if(urlContainer == null)
                {
                    break;
                }
                foreach(var photoUrl in urlContainer)
                {
                    Console.WriteLine($"Download of the {counter} url started");
                    var pathToImageBeforeResizing = controller.CreatePathToImageBeforeResizing(photoUrl);
                    controller.DownloadImage(photoUrl, pathToImageBeforeResizing);
                    Console.WriteLine($"Downloading of the {counter} url finished");

                    Console.WriteLine($"Resizing of the {counter} url started");
                    var pathToImageAfterResizing = controller.CreatePathToImageAfterResizing(pathToImageBeforeResizing
                        .Split(@"\").Last());
                    controller.ResizePhoto(pathToImageBeforeResizing, pathToImageAfterResizing);
                    Console.WriteLine($"Resizing of the {counter} url finished");

                    controller.PutIntoDatabase(new Models.ImageWithUrl { ImageId = pathToImageAfterResizing, PhotoUrl = photoUrl });

                    Console.WriteLine("/////////////////////////////////////////////////////");
                }
            }
            while (urlContainer.Any());

            Console.ReadKey();
        }
    }
}
