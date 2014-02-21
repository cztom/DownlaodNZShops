using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DownloadNZShops
{
    public class Shop
    {
        [BsonId]
        public ObjectId _id { get; set; }
        /// <summary>
        /// 店名
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 座机
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Google地图位置
        /// </summary>
        public string Map { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        public string WebSite { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 营业时间
        /// </summary>
        public string BusinessHours { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 是否是VIP店铺，默认不是VIP
        /// </summary>
        public bool IsVip { get; set; }
        /// <summary>
        /// 是否是总店，默认是总店
        /// </summary>
        public bool IsHeadquarters { get; set; }
        /// <summary>
        /// 是否被认证，默认是没有
        /// </summary>
        public bool IsCertified { get; set; }
        /// <summary>
        /// 原始链接地址
        /// </summary>
        public string OriginalUrl { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
    }
}
