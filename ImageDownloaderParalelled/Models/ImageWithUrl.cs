using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageDownloaderParalelled.Models
{
    public class ImageWithUrl
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PhotoUrl { get; set; }
        public string ImageId { get; set; } 

    }
}
