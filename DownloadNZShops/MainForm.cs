using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HtmlAgilityPack;
using log4net;
using System.Timers;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace DownloadNZShops {
    public partial class MainForm : Form {
        //日志记录
        ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public MainForm() {
            InitializeComponent();
        }

        private void btnFatherCategory_Click(object sender, EventArgs e) {
            string[] arrFatherCategory = { "餐馆美食", "实用信息", "休闲娱乐", "美容保健", "超市商店", "电脑网络", "房屋地产", "金融保险", "教育移民", "旅游指南", "汽车市场", "商业服务" };
            int index = 0;
            foreach (var item in arrFatherCategory) {
                Category category = new Category {
                    Id = index + 1,
                    Name = arrFatherCategory[index],
                    FatherId = 0,
                    OrderId = index + 10
                };
                MongoDBHelper db = new MongoDBHelper();
                db.Insert<Category>(category);
                index++;
            }
            MessageBox.Show("添加大分类成功");
        }

        private void btnSonCategory_Click(object sender, EventArgs e) {
            #region 分析网页html节点
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("gbk");
            HtmlAgilityPack.HtmlDocument doc = web.Load("http://opage.skykiwi.com/");
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@id='m_left']//li");
            int index = 0;
            MongoDBHelper db = new MongoDBHelper();
            for (int i = 0; i < nodes.Count; i++) {
                var links = nodes[i].Descendants("a").ToList();
                foreach (var link in links) {
                    string name = link.InnerText;
                    string href = link.Attributes["href"].Value;
                    string id = href.Substring(href.Length - 4, 4);
                    Category category = new Category {
                        Id = index + 1000,
                        Name = name,
                        FatherId = i + 1,
                        OrderId = int.Parse(id)
                    };
                    db.Insert<Category>(category);
                    index++;
                }
            }
            MessageBox.Show("添加小分类成功");
            #endregion
        }

        #region fetch shop links
        private void btnGetShopLink_Click(object sender, EventArgs e) {
            this.btnGetShopLink.Enabled = false;
            MongoDBHelper db = new MongoDBHelper();
            List<Category> categorys = db.FindAll<Category>().FindAll(p => p.OrderId > 1000).OrderBy(m => m.Id).ToList();
            Thread mainShopLinkThread = new Thread(new ThreadStart(delegate {
                ShopLinkThread(categorys);
            }));
            mainShopLinkThread.IsBackground = true;
            mainShopLinkThread.Start();
        }

        public void ShopLinkThread(List<Category> categorys) {
            try {
                MongoDBHelper db = new MongoDBHelper();
                foreach (var item in categorys) {
                    string url = "http://opage.skykiwi.com/shop/cate.php?cd=" + item.OrderId;

                    HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                    web.OverrideEncoding = Encoding.GetEncoding("gbk");
                    HtmlAgilityPack.HtmlDocument doc = web.Load(url);
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@id='pg_list']//a");
                    if (nodes != null && nodes.Count != 0) {
                        var node = nodes.Last();
                        int maxPage = int.Parse(node.Attributes["href"].Value.Replace("cate.php?cd=" + item.OrderId + "&amp;page=", ""));

                        //log.Info("http://opage.skykiwi.com/shop/cate.php?cd=" + item.OrderId + "&page=" + maxPage);

                        //loop each page
                        for (int i = 1; i <= maxPage; i++) {
                            string categoryUrl = "http://opage.skykiwi.com/shop/cate.php?cd=" + item.OrderId + "&page=" + i;
                            List<Dictionary<string, string>> shopList = new List<Dictionary<string, string>>();
                            shopList = GetShopLinks(categoryUrl);
                            foreach (var entry in shopList) {
                                ShopLink sl = new ShopLink {
                                    Href = entry.First().Key,
                                    Name = entry.First().Value,
                                    CategoryID = item.Id.ToString(),
                                    IsRead = false
                                };
                                db.Insert<ShopLink>(sl);
                            }
                        }
                    } else {
                        log.Info(url);

                        List<Dictionary<string, string>> shopList = GetShopLinks(url);
                        foreach (var entry in shopList) {
                            ShopLink sl = new ShopLink {
                                Href = entry.First().Key,
                                Name = entry.First().Value,
                                CategoryID = item.Id.ToString(),
                                IsRead = false
                            };
                            db.Insert<ShopLink>(sl);
                        }
                    }
                }
                this.btnGetShopLink.Enabled = true;
                MessageBox.Show("Add Shop Links Successful!");
            } catch (Exception ex) {
                log.Error("错误信息：" + ex.ToString() + "\r\n\r\n");
            }
        }

        private List<Dictionary<string, string>> GetShopLinks(string url) {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("gbk");
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@id='m_shop_list']//li//h3//a");
            if (nodes != null && nodes.Count != 0) {
                foreach (var node in nodes) {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add(node.Attributes["href"].Value, node.InnerText);
                    list.Add(dic);
                }
            }
            return list;
        }
        #endregion


        #region fetch shops details
        int count = 0;
        int totalCount = 0;
        //Fetch Elapsed Time
        long elaspsedMilliseconds = 0;
        ////创建一个委托，是为访问控件服务的。
        //public delegate void RefreshControlValueDelegate(int count, string href);
        ////定义一个委托变量
        //public RefreshControlValueDelegate refreshControlValueDelegate;

        
        //主程序运行线程
        private Thread mainThread;
        private Stopwatch stopWatch;
        private DateTime now;
        private System.Windows.Forms.Timer timer;
        private void btnFetch_Click(object sender, EventArgs e) {
            stopWatch = Stopwatch.StartNew();

            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Start();
            now = DateTime.Now;

            this.btnFetch.Enabled = false;
            this.btnStop.Enabled = true;

            //refreshControlValueDelegate = new RefreshControlValueDelegate(RefreshControlValueMethod);

            MongoDBHelper db = new MongoDBHelper();
            List<ShopLink> shopLinks = db.FindAll<ShopLink>().FindAll(p => p.IsRead == false);
            totalCount = shopLinks.Count;
            this.proBarDispose.Maximum = totalCount;

            if (mainThread == null || mainThread.ThreadState == System.Threading.ThreadState.Aborted || mainThread.ThreadState == System.Threading.ThreadState.Stopped) {
                mainThread = new Thread(new ThreadStart(delegate {
                    FetchShopLinksThread(shopLinks);
                }));
                mainThread.IsBackground = true;
                mainThread.Start();
            }
        }

        public void FetchShopLinksThread(List<ShopLink> shopLinks) {
            foreach (var item in shopLinks) {
                //this.BeginInvoke(refreshControlValueDelegate, count, item.Href);

                DownLoadShopDetails(item);
                count++;

                //refresh controls value
                this.proBarDispose.SetPropertyThreadSafe(a => a.Value, count);
                this.labProcessBar.SetPropertyThreadSafe(a => a.Text, string.Format("{0}/{1}", count, totalCount));
                this.lnkLabProcessUrl.SetPropertyThreadSafe(a => a.Text, item.Href);

                Thread.Sleep(50);
            }
            if (stopWatch.IsRunning) {
                timer.Stop();

                stopWatch.Stop();
                elaspsedMilliseconds = stopWatch.ElapsedMilliseconds;
                this.labElapsedTime.SetPropertyThreadSafe(a => a.Text, Common.FormatSeconds(elaspsedMilliseconds, false));
            }
            MessageBox.Show("全部抓取完毕！");
        }

        private void timer_Tick(object sender, EventArgs e) {
            TimeSpan ts1 = new TimeSpan(now.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            labElapsedTime.Text = ts.Days + "天" + ts.Hours + "小时" + ts.Minutes + "分钟" + ts.Seconds + "秒" + ts.Milliseconds + "毫秒";
            labElapsedTime.Refresh();
        }

        public void RefreshControlValueMethod(int count, string href) {
            this.proBarDispose.Value = count;
            this.labProcessBar.Text = string.Format("{0}/{1}", count, totalCount);
            this.lnkLabProcessUrl.Text = href;
        }

        public void DownLoadShopDetails(ShopLink shopLink) {
            Shop shop = new Shop();
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("gbk");
            HtmlAgilityPack.HtmlDocument doc = web.Load(shopLink.Href);
            shop.Title = doc.DocumentNode.SelectSingleNode("//div[@id='s_title']/h2").InnerText;
            shop.Avatar = doc.DocumentNode.SelectSingleNode("//div[@id='s_con_pic']//img").GetAttributeValue("src", "http://opage.skykiwi.com/images/noimg.gif");
            shop.Map = doc.DocumentNode.SelectSingleNode("//iframe[@id='map_iframe']").GetAttributeValue("src", "").Replace("&amp;", "&");
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@id='s_con_info']//p");

            foreach (HtmlNode node in nodes) {
                HtmlNode htmlNode = node.FirstChild;
                if (htmlNode.InnerText.Contains("电话：")) {
                    shop.Telephone = htmlNode.NextSibling.InnerText.Replace("&nbsp;", "").Trim();
                }
                if (htmlNode.InnerText.Contains("手机：")) {
                    shop.Mobile = htmlNode.NextSibling.InnerText;
                }
                if (htmlNode.InnerText.Contains("Email：")) {
                    shop.Email = htmlNode.NextSibling.InnerText;
                }
                if (htmlNode.InnerText.Contains("传真：")) {
                    shop.Fax = htmlNode.NextSibling.InnerText;
                }
                if (htmlNode.InnerText.Contains("地址：")) {
                    shop.Address = htmlNode.NextSibling.InnerText;
                }
                if (htmlNode.InnerText.Contains("网址：")) {
                    shop.WebSite = htmlNode.NextSibling.InnerText;
                }
            }
            HtmlNode hn = doc.DocumentNode.SelectSingleNode("//span[@id='shop-content']");
            if (hn != null) {
                shop.Description = hn.InnerHtml;
            }
            shop.Category = shopLink.CategoryID;
            shop.IsVip = false;
            if (shop.Title.Contains("总店")) {
                shop.IsHeadquarters = true;
            } else {
                shop.IsHeadquarters = false;
            }
            shop.IsCertified = false;
            shop.BusinessHours = null;
            shop.OriginalUrl = shopLink.Href;
            shop.LastModified = DateTime.Now;

            MongoDBHelper db = new MongoDBHelper();
            db.Insert<Shop>(shop);
            shopLink.IsRead = true;
            db.Update<ShopLink>(shopLink);
        }
        #endregion

        private void btnStop_Click(object sender, EventArgs e) {
            if (mainThread != null && mainThread.IsAlive)
                mainThread.Abort();

            count = 0;
            //labProcessBar.Text = "0/0";
            proBarDispose.Value = 0;
            this.btnFetch.Enabled = true;
            this.btnStop.Enabled = false;

            timer.Stop();

            stopWatch.Stop();
            elaspsedMilliseconds = stopWatch.ElapsedMilliseconds;
            labElapsedTime.SetPropertyThreadSafe(a => a.Text, Common.FormatSeconds(elaspsedMilliseconds, false));
        }

        private void MainForm_Load(object sender, EventArgs e) {
            this.btnStop.Enabled = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            if (mainThread != null && mainThread.IsAlive)
                mainThread.Abort();
        }

        private void btnNewForm_Click(object sender, EventArgs e) {
            TraceForm traceForm = new TraceForm();
            traceForm.Show();
            this.Hide();
        }
    }
}
