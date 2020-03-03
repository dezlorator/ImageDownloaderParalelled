using ImageDownloaderParalelled.Interfaces;
using ImageDownloaderParalelled.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloaderParalelled.Services
{
    class MongoDBService : IMongoDBService
    {
        #region fieds
        private readonly IMongoDbRepository mongoDbRepository;
        #endregion

        public MongoDBService(IMongoDbRepository mongoDbRepository)
        {
            this.mongoDbRepository = mongoDbRepository;
        }

        public async Task Create(ImageWithUrl imageWithUrl)
        {
            string id = await mongoDbRepository.Create(imageWithUrl);
            using (Stream fs = new FileStream(imageWithUrl.ImageId, FileMode.Open))
            {
                await mongoDbRepository.StoreImage(id, fs, imageWithUrl.PhotoUrl.Split(@"\").Last());
            }
        }
    }
}
