using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DownloadNZShops
{
    public class ShopLink
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public string CategoryID { get; set; }
        public bool IsRead { get; set; }
    }
}