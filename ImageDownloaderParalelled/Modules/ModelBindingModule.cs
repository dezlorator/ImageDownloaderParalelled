using ImageDownloaderParalelled.Convertors;
using ImageDownloaderParalelled.Interfaces;
using ImageDownloaderParalelled.Repositories;
using ImageDownloaderParalelled.Services;
using ImageDownloaderParalelled.Validators;
using MongoDB.Driver;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
            Bind<IImageDownloaderService>().To<ImageDownloaderService>();
            Bind<IController>().To<Controller>();
            Bind<IFileValidator>().To<FileValidator>();
            Bind<IConvertor<string, Uri>>().To<UrlConvertor>();
            Bind<IPathCreator>().To<PathCreator>();
            Bind<IResizeImageService>().To<ResizeImageService>();
            Bind<IMongoDbRepository>().To<MongoDbRepository>();
            Bind<IMongoDBService>().To<MongoDBService>();
            Bind<HttpClient>().ToSelf();
        }
    }
}
