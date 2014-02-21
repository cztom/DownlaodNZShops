using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace DownloadNZShops {
    public partial class TraceForm : Form {
        public TraceForm() {
            InitializeComponent();
        }

        string path = Application.StartupPath + "\\novel.txt";
        private void TraceForm_Load(object sender, EventArgs e) {
            LoadConfig();
        }

        private void LoadConfig() {
            if (!File.Exists(path)) {
                List<NovelConfig> items = new List<NovelConfig>();
                items.Add(new NovelConfig {
                    Url = "http://www.2100game.com/0_252/0.html",
                    LastChapterNo = "2098",
                    ListXPath = "//div[@id='list']//a",
                    ContentXPath = "//div[@id='content']//p"
                });
                items.Add(new NovelConfig {
                    Url = "http://www.mdxs.com/xiaoshuo/html/0/306/index.html",
                    LastChapterNo = "2098",
                    ListXPath = "//div[@id='readtext']//a",
                    ContentXPath = "//div[@id='readtext']"
                });
                File.WriteAllText(path, JsonConvert.SerializeObject(items));
                foreach (var item in items) {
                    this.cbBoxUrl.Items.Add(item.Url);
                    this.cbBoxChapter.Items.Add(item.LastChapterNo);
                    this.cbBoxXPath.Items.Add(item.ListXPath);
                    this.cbBoxContentXPath.Items.Add(item.ContentXPath);
                }
                this.cbBoxUrl.SelectedIndex = 0;
                this.cbBoxXPath.SelectedIndex = 0;
                this.cbBoxChapter.SelectedIndex = 0;
                this.cbBoxContentXPath.SelectedIndex = 0;
            } else {
                using (StreamReader sr = new StreamReader(path, Encoding.Default)) {
                    List<NovelConfig> items = JsonConvert.DeserializeObject<List<NovelConfig>>(sr.ReadToEnd());
                    foreach (var item in items) {
                        this.cbBoxUrl.Items.Add(item.Url);
                        this.cbBoxChapter.Items.Add(item.LastChapterNo);
                        this.cbBoxXPath.Items.Add(item.ListXPath);
                        this.cbBoxContentXPath.Items.Add(item.ContentXPath);
                    }
                    this.cbBoxUrl.SelectedIndex = 0;
                    this.cbBoxXPath.SelectedIndex = 0;
                    this.cbBoxChapter.SelectedIndex = 0;
                    this.cbBoxContentXPath.SelectedIndex = 0;
                }
            }
        }

        private void TraceForm_FormClosed(object sender, FormClosedEventArgs e) {
            DialogResult dialog = MessageBox.Show("Are you sure to close?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) {
                Application.Exit();
            } else {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
        }

        public delegate void AddControlDelegate();
        private void btnTrace_Click(object sender, EventArgs e) {
            btnTrace.Enabled = false;
            Thread thread = new Thread(TraceNovel);
            thread.IsBackground = true;
            thread.Start();
        }

        private delegate object getComoboText();
        private object getSelectedComboUrlText() {
            if (cbBoxUrl.InvokeRequired) {
                getComoboText gct = new getComoboText(getSelectedComboUrlText);
                return cbBoxUrl.Invoke(gct);
            } else {
                return cbBoxUrl.Text;
            }
        }
        private object getSelectedComboChapterText() {
            if (cbBoxChapter.InvokeRequired) {
                getComoboText gct = new getComoboText(getSelectedComboChapterText);
                return cbBoxChapter.Invoke(gct);
            } else {
                return cbBoxChapter.Text;
            }
        }
        private object getSelectedComboXPathText() {
            if (cbBoxXPath.InvokeRequired) {
                getComoboText gct = new getComoboText(getSelectedComboXPathText);
                return cbBoxXPath.Invoke(gct);
            } else {
                return cbBoxXPath.Text;
            }
        }

        private void TraceNovel() {
            Stopwatch stopWatch = Stopwatch.StartNew();
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("gbk");
            HtmlAgilityPack.HtmlDocument doc = web.Load(getSelectedComboUrlText().ToString());
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(getSelectedComboXPathText().ToString());

            List<Chapter> chapters = new List<Chapter>();
            int oldChapter = int.Parse(getSelectedComboChapterText().ToString());
            foreach (var node in nodes) {
                int newChapter = Common.GetFirstNumberFromString(node.InnerText);
                if (newChapter > oldChapter) {
                    string href = node.Attributes["href"].Value;
                    chapters.Add(new Chapter {
                        Id = int.Parse(Common.GetUrlFileName(href).Replace(".html", "")),
                        Title = node.InnerText,
                        Href = href
                    });
                }
            }
            if (chapters.Count > 0) {
                LinkLabel[] LnkLbl = new LinkLabel[chapters.Count];
                int i = 0;
                this.Invoke((AddControlDelegate)delegate() {
                    panel1.Controls.Clear();
                    foreach (var item in chapters) {
                        LnkLbl[i] = new LinkLabel();
                        LnkLbl[i].Text = item.Title;
                        LnkLbl[i].Width = panel1.Width / 5;
                        LnkLbl[i].Left = LnkLbl[i].Width * i;
                        LnkLbl[i].Height = panel1.Height;
                        LnkLbl[i].Top = 0;
                        LnkLbl[i].Name = "LnkLbl" + item.Id.ToString();
                        LnkLbl[i].LinkClicked += new LinkLabelLinkClickedEventHandler(this.LinkLabelArray_OnClick);
                        panel1.Controls.Add(LnkLbl[i]);
                        i++;
                    }
                    btnTrace.Enabled = true;
                    stopWatch.Stop();
                    label3.Text = "共耗时：" + Common.FormatSeconds(stopWatch.ElapsedMilliseconds, false);
                });
            } else {
                this.Invoke((AddControlDelegate)delegate() {
                    panel1.Controls.Clear();
                    Label lbl = new Label();
                    lbl.Text = "还没有小说更新哟！";
                    lbl.Width = panel1.Width;
                    lbl.Left = 0;
                    lbl.Height = panel1.Height;
                    lbl.Top = 0;
                    lbl.Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
                    lbl.ForeColor = Color.Red;
                    lbl.Name = "lbl";
                    panel1.Controls.Add(lbl);
                    btnTrace.Enabled = true;
                    stopWatch.Stop();
                    label3.Text = "共耗时：" + Common.FormatSeconds(stopWatch.ElapsedMilliseconds, false);
                });
            }
        }

        private void LinkLabelArray_OnClick(object sender, LinkLabelLinkClickedEventArgs e) {
            LinkLabelLinkClickedEventArgs arg = (LinkLabelLinkClickedEventArgs)e;
            LinkLabel lnkLbl = (LinkLabel)sender;
            if (arg.Button == MouseButtons.Left) {
                string uriString = cbBoxUrl.Text.Trim();
                string url = uriString.Substring(0, uriString.LastIndexOf('/') + 1) + lnkLbl.Name.Replace("LnkLbl", "") + ".html";
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                web.OverrideEncoding = Encoding.GetEncoding("gbk");
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);
                HtmlNode node = doc.DocumentNode.SelectSingleNode(cbBoxContentXPath.Text);

                richTextBox1.Text = node.InnerText.Replace("&nbsp;", " ");
                int number = Common.GetFirstNumberFromString(lnkLbl.Text);
                //记录最后看的章节
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                List<NovelConfig> items = new List<NovelConfig>();
                for (int i = 0; i < cbBoxUrl.Items.Count; i++) {
                    //开始写入
                    items.Add(new NovelConfig {
                        Url = cbBoxUrl.Items[i].ToString(),
                        LastChapterNo = number.ToString(),
                        ListXPath = cbBoxXPath.Items[i].ToString(),
                        ContentXPath = cbBoxContentXPath.Items[i].ToString()
                    });
                }
                sw.Write(JsonConvert.SerializeObject(items));
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
        }

        private void cbBoxUrl_SelectedIndexChanged(object sender, EventArgs e) {
            cbBoxChapter.SelectedIndex = cbBoxUrl.SelectedIndex;
            cbBoxXPath.SelectedIndex = cbBoxUrl.SelectedIndex;
            cbBoxContentXPath.SelectedIndex = cbBoxUrl.SelectedIndex;
        }

        private void cbBoxXPath_SelectedIndexChanged(object sender, EventArgs e) {
            cbBoxChapter.SelectedIndex = cbBoxXPath.SelectedIndex;
            cbBoxUrl.SelectedIndex = cbBoxXPath.SelectedIndex;
            cbBoxContentXPath.SelectedIndex = cbBoxXPath.SelectedIndex;
        }

        private void cbBoxContentXPath_SelectedIndexChanged(object sender, EventArgs e) {
            cbBoxChapter.SelectedIndex = cbBoxContentXPath.SelectedIndex;
            cbBoxUrl.SelectedIndex = cbBoxContentXPath.SelectedIndex;
            cbBoxXPath.SelectedIndex = cbBoxContentXPath.SelectedIndex;
        }
    }

    public class Chapter {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
    }
}
