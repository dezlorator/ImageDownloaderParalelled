using ClosedXML.Excel;
using ImageDownloaderParalelled.Interfaces;
using ImageDownloaderParalelled.Modules;
using ImageDownloaderParalelled.Services;
using ImageDownloaderParalelled.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ImageDownloaderParalelled
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToFile = System.IO.Directory.GetCurrentDirectory() + @"\Excel\WithoutCopy.xlsx";

            var kernel = new StandardKernel(new ModelBindingModule());
            var controller = kernel.Get<IController>();
            IEnumerable<string> urlContainer;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                urlContainer = controller.GetUrlFromFile(pathToFile);
                Console.WriteLine($"{urlContainer.Count()} was downloaded from the file");
                if(urlContainer == null)
                {
                    break;
                }

                MainAsync(urlContainer, controller).Wait();
            }
            while (urlContainer.Any());
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            Console.ReadKey();
        }

        public static async Task MainAsync(IEnumerable<string> urlContainer, IController controller)
        {
            int numberOfUrlByStep = 5;
            int numberOfProsessedUrl = 0;

            while (numberOfProsessedUrl < urlContainer.Count())
            {
                var prosessedUrl = urlContainer.Skip(numberOfProsessedUrl).Take(numberOfUrlByStep);
                var tasks = prosessedUrl.Select(p => ProcessPhoto(p, controller));
                await Task.WhenAll(tasks);
                numberOfProsessedUrl += numberOfUrlByStep;
            }
        }

        public static async Task ProcessPhoto(string photoUrl, IController controller)
        {
            Console.WriteLine($"Download of the {photoUrl.Split(@"\").Last()} url started");
            var pathToImageBeforeResizing = controller.CreatePathToImageBeforeResizing(photoUrl);
            await controller.DownloadImage(photoUrl, pathToImageBeforeResizing);
            Console.WriteLine($"Downloading of the {photoUrl.Split(@"\").Last()} url finished");

            Console.WriteLine($"Resizing of the {photoUrl.Split(@"\").Last()} url started");
            var pathToImageAfterResizing = controller.CreatePathToImageAfterResizing(pathToImageBeforeResizing
                .Split(@"\").Last());
            controller.ResizePhoto(pathToImageBeforeResizing, pathToImageAfterResizing);
            Console.WriteLine($"Resizing of the {photoUrl.Split(@"\").Last()} url finished");

            Console.WriteLine($"Downloading into database {photoUrl.Split(@"\").Last()} url started");
            await controller.PutIntoDatabase(new ImageWithUrl { PhotoUrl = photoUrl }, pathToImageAfterResizing);
            Console.WriteLine($"Downloading into database {photoUrl.Split(@"\").Last()} url finished");
        }
    }
}