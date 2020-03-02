using ImageDownloaderParalelled.Convertors;
using ImageDownloaderParalelled.Interfaces;
using ImageDownloaderParalelled.Services;
using ImageDownloaderParalelled.Validators;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ImageDownloaderParalelled.Modules
{
    class ModelBindingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<WebClient>().ToSelf();
            Bind<IStepCounterService>().To<StepCounterService>();
            Bind<IExcelPhotoURLGetterService>().To<ExcelPhotoURLGetterService>();
            Bind<IController>().To<Controller>();
            Bind<IFileValidator>().To<FileValidator>();
            Bind<IConvertor<string, Uri>>().To<UrlConvertor>();
        }
    }
}
