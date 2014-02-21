using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

namespace DownloadNZShops
{
    public class WebSpider
    {
        private ShopLink _shopLink;
        public WebSpider(ShopLink shopLink)
        {
            this._shopLink = shopLink;
        }

        public void DownLoad()
        {
            Shop shop = new Shop();
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("gbk");
            HtmlAgilityPack.HtmlDocument doc = web.Load(_shopLink.Href);
            shop.Title = doc.DocumentNode.SelectSingleNode("//div[@id='s_title']/h2").InnerText;
            shop.Avatar = doc.DocumentNode.SelectSingleNode("//div[@id='s_con_pic']//img").GetAttributeValue("src", "http://opage.skykiwi.com/images/noimg.gif");
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@id='s_con_info']//p");
            
            foreach (HtmlNode node in nodes)
            {
                HtmlNode htmlNode = node.FirstChild;
                if (htmlNode.InnerText.Contains("电话"))
                {
                    shop.Telephone = node.FirstChild.NextSibling.InnerText.Replace("&nbsp;", "").Trim();
                }
                if (htmlNode.InnerText.Contains("手机"))
                {
                    shop.Mobile = node.FirstChild.NextSibling.InnerText;
                }
                if (htmlNode.InnerText.Contains("Email"))
                {
                    shop.Email = node.FirstChild.NextSibling.InnerText;
                }
                if (htmlNode.InnerText.Contains("传真"))
                {
                    shop.Fax = node.FirstChild.NextSibling.InnerText;
                }
                if (htmlNode.InnerText.Contains("地址"))
                {
                    shop.Address = node.FirstChild.NextSibling.InnerText;
                }
                if (htmlNode.InnerText.Contains("网址"))
                {
                    shop.WebSite = node.FirstChild.NextSibling.InnerText;
                }
            }
            HtmlNode hn = doc.DocumentNode.SelectSingleNode("//span[@id='shop-content']");
            if (hn != null)
            {
                shop.Description = hn.InnerHtml;
            }
            shop.Category = _shopLink.CategoryID;
            shop.IsVip = false;
            if (shop.Title.Contains("总店"))
            {
                shop.IsHeadquarters = true;
            }
            else
            {
                shop.IsHeadquarters = false;
            }
            shop.IsCertified = false;
            shop.CreateTime = DateTime.Now;

            //MongoDBHelper db = new MongoDBHelper();
            //db.Insert<Shop>(shop);
        }
    }
}