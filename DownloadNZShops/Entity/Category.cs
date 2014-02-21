using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DownloadNZShops
{
    /// <summary>
    /// 分类
    /// </summary>
    public class Category
    {
        [BsonId]
        public ObjectId _id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父节点编号
        /// </summary>
        public int FatherId { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        public int OrderId { get; set; }
    }
}
