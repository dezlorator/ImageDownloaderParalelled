using ImageDownloaderParalelled.Interfaces;
using ImageDownloaderParalelled.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloaderParalelled.Repositories
{
    class MongoDbRepository : IMongoDbRepository
    {
        #region fields
        private const string dbName = "ImagesDatabase";
        private const string collectionName = "NewImageWithUrl";
        private const string connectionString = 
            "mongodb+srv://admin:123@cluster0-tpr1e.azure.mongodb.net/test?retryWrites=true&w=majority";
        IGridFSBucket gridFS;   // файловое хранилище
        IMongoCollection<ImageWithUrl> imagesWithUrl; // коллекция в базе данных
        #endregion

        #region ctor
        public MongoDbRepository()
        {
            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(connectionString);
            // получаем доступ к самой базе данных
            IMongoDatabase database = client.GetDatabase(dbName);
            // получаем доступ к файловому хранилищу
            gridFS = new GridFSBucket(database);
            // обращаемся к коллекции Products
            imagesWithUrl = database.GetCollection<ImageWithUrl>(collectionName);
        }
        #endregion

        // получаем один документ по id
        public async Task<ImageWithUrl> GetProduct(string id)
        {
            return await imagesWithUrl.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }
        // добавление документа
        public async Task<string> Create(ImageWithUrl p)
        {
            await imagesWithUrl.InsertOneAsync(p);
            return p.Id;
        }
        // обновление документа
        public async Task Update(ImageWithUrl p)
        {
            await imagesWithUrl.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(p.Id)), p);
        }
        // удаление документа
        public async Task Remove(string id)
        {
            await imagesWithUrl.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
        // получение изображения
        public async Task<byte[]> GetImage(string id)
        {
            return await gridFS.DownloadAsBytesAsync(new ObjectId(id));
        }
        // сохранение изображения
        public async Task StoreImage(string id, Stream imageStream, string imageName)
        {
            ImageWithUrl p = await GetProduct(id);
            if (p.HasImage())
            {
                // если ранее уже была прикреплена картинка, удаляем ее
                await gridFS.DeleteAsync(new ObjectId(p.ImageId));
            }
            // сохраняем изображение
            ObjectId imageId = await gridFS.UploadFromStreamAsync(imageName, imageStream);
            // обновляем данные по документу
            p.ImageId = imageId.ToString();
            var filter = Builders<ImageWithUrl>.Filter.Eq("_id", new ObjectId(p.Id));
            var update = Builders<ImageWithUrl>.Update.Set("ImageId", p.ImageId);
            await imagesWithUrl.UpdateOneAsync(filter, update);
        }
    }
}
