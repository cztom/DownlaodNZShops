using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DownloadNZShops {
    public class Common {
        /// <summary>
        /// 根据链接地址获取网页源代码
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns></returns>
        public static string GetPageSource(string url) {
            try {
                string str = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Set("Pragma", "no-cache");
                request.Timeout = 30000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK && response.ContentLength < 1024 * 1024) {
                    Stream strM = response.GetResponseStream();
                    StreamReader sr = new StreamReader(strM, Encoding.GetEncoding("gbk"));
                    str = sr.ReadToEnd();
                    strM.Close();
                    sr.Close();
                }
                return str;
            }
            catch {
                return String.Empty;
            }
        }

        /// <summary>
        /// 根据链接地址--获取标题
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns></returns>
        public static string GetTitle(string url) {
            string title = string.Empty;
            string htmlStr = GetPageSource(url);//获取网页
            Match TitleMatch = Regex.Match(htmlStr, "<title>([^<]*)</title>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            title = TitleMatch.Groups[1].Value;
            title = Regex.Replace(title, @"\W", "");//去除空格

            return title;
        }

        /// <summary>
        /// 根据链接地址--获取描述信息
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns></returns>
        public static string GetDescription(string url) {
            string htmlStr = GetPageSource(url);
            Match Desc = Regex.Match(htmlStr, "<meta name=\"Description\" content=\"([^<]*)\"*>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string mdd = Desc.Groups[1].Value;
            return Regex.Replace(Desc.Groups[1].Value, @"\W", "");
        }

        /// <summary>
        /// 根据网页源代码--获取所有链接
        /// </summary>
        /// <param name="html">网页源代码</param>
        /// <returns></returns>
        public static List<string> GetLinks(string html) {
            List<string> list = new List<string>(); //用来存放链接       
            String reg = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";  //链接的正则表达式      
            Regex regex = new Regex(reg, RegexOptions.IgnoreCase);
            MatchCollection mc = regex.Matches(html);
            for (int i = 0; i < mc.Count; i++) //存放匹配的集合
            {
                bool hasExist = false;   //链接存在与否的标记         
                String name = mc[i].ToString();
                foreach (String one in list) {
                    if (name == one) {
                        hasExist = true; //链接已存在                   
                        break;
                    }
                }
                if (!hasExist) list.Add(name); //链接不存在，添加
            }
            return list;
        }

        /// <summary>
        /// 根据链接地址--取得body内的内容
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns></returns>
        public static string GetBody(string url) {
            string htmlStr = GetPageSource(url);
            string result = string.Empty;
            Regex regBody = new Regex(@"(?is)<body[^>]*>(?:(?!</?body\b).)*</body>");
            Match m = regBody.Match(htmlStr);
            if (m.Success) {
                result = FilterHtml(m.Value);
            }
            return result;
        }

        //获取所有图片
        public static List<string> GetAllImg(string url) {
            List<string> list = new List<string>();
            string temp = string.Empty;
            string htmlStr = GetPageSource(url);
            MatchCollection matchs = Regex.Matches(htmlStr, @"<(IMG|img)[^>]+>"); //抽取所有图片
            for (int i = 0; i < matchs.Count; i++) {
                list.Add(matchs[i].Value);
            }
            return list;
        }

        /// <summary>
        /// 所有图片路径(如果是相对路径的话，自动设置成绝对路径)
        /// </summary>
        /// <param name="url">网页链接</param>
        /// <returns></returns>
        public static List<string> GetAllImgUrl(string url) {
            List<string> list = new List<string>();
            string htmlStr = GetPageSource(url);
            string pat = @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>";
            MatchCollection matches = Regex.Matches(htmlStr, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (Match m in matches) {
                string imgPath = m.Groups["imgUrl"].Value.Trim();
                if (Regex.IsMatch(imgPath, @"\w+\.(gif|jpg|bmp|png)$")) //用了2次匹配，去除链接是网页的 只留图片
                {
                    if (!imgPath.Contains("http"))//必须包含http 否则无法下载
                    {
                        imgPath = ProcessUrl(url) + imgPath;
                    }
                    list.Add(imgPath);
                }
            }
            return list;
        }

        /// <summary>
        /// 处理url路径问题
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns></returns>
        public static string ProcessUrl(string url) {
            //如果是http://www.xxx.com           返回http://www.xxx.com/
            //如果是http://www.xxx.com/art.aspx  返回http://www.xxx.com/
            return url = url.Substring(0, url.LastIndexOf('/')) + "/";
        }

        /// <summary>
        /// 过滤html
        /// </summary>
        /// <param name="html">网页源代码</param>
        /// <returns></returns>
        public static string FilterHtml(string html) {
            string value = Regex.Replace(html, "<[^>]*>", string.Empty);
            value = value.Replace("<", string.Empty);
            value = value.Replace(">", string.Empty);
            //return value.Replace("&nbsp;", string.Empty);

            return Regex.Replace(value, @"\s+", "");
        }

        /// <summary>
        /// 此私有方法从一段HTML文本中提取出一定字数的纯文本
        /// </summary>
        /// <param name="html">HTML代码</param>
        /// <param name="firstN">提取从头数多少个字</param>
        /// <param name="withLink">是否要链接里面的字</param>
        /// <returns>纯文本</returns>
        public static string GetFirstNchar(string html, int firstN, bool withLink) {
            string outPutString = "";
            outPutString = html.Clone() as string;
            outPutString = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(outPutString, "");
            outPutString = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(outPutString, "");
            outPutString = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(outPutString, "");
            if (!withLink) outPutString = new Regex(@"(?m)<a[^>]*>(\w|\W)*?</a[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(outPutString, "");
            Regex objReg = new System.Text.RegularExpressions.Regex("(<[^>]+?>)|&nbsp;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            outPutString = objReg.Replace(outPutString, "");
            Regex objReg2 = new System.Text.RegularExpressions.Regex("(\\s)+", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            outPutString = objReg2.Replace(outPutString, " ");

            return outPutString.Length > firstN ? outPutString.Substring(0, firstN) : outPutString;
        }

        /// <summary>
        /// 此公有方法提取网页中一定字数的纯文本，包括链接文字
        /// </summary>
        /// <param name="html">HTML代码</param>
        /// <param name="firstN">字数</param>
        /// <returns></returns>
        public string GetContext(string html, int firstN) {
            return GetFirstNchar(html, firstN, true);
        }

        /// <summary>
        /// 这公有方法提取本网页的纯文本中满足某正则式的文字
        /// </summary>
        /// <param name="html">HTML代码</param>
        /// <param name="pattern">正则式</param>
        /// <returns>返回文字</returns>
        public string GetSpecialWords(string html, string pattern) {
            if (html == "") GetContext(html, Int16.MaxValue);
            Regex regex = new Regex(pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            Match mc = regex.Match(html);
            if (mc.Success)
                return mc.Groups[1].Value;
            return string.Empty;
        }

        public static string FormatSeconds(long ms, bool moreZero) {
            int ss = 1000;
            int mi = ss * 60;
            int hh = mi * 60;
            int dd = hh * 24;

            long day = ms / dd;
            long hour = (ms - day * dd) / hh;
            long minute = (ms - day * dd - hour * hh) / mi;
            long second = (ms - day * dd - hour * hh - minute * mi) / ss;
            long milliSecond = ms - day * dd - hour * hh - minute * mi - second * ss;

            string strDay = "";
            string strHour = "";
            string strMinute = "";
            string strSecond = "";
            string strMilliSecond = "";
            if (moreZero) {
                strDay = day < 10 ? "0" + day : "" + day;
                strHour = hour < 10 ? "0" + hour : "" + hour;
                strMinute = minute < 10 ? "0" + minute : "" + minute;
                strSecond = second < 10 ? "0" + second : "" + second;
                strMilliSecond = milliSecond < 10 ? "0" + milliSecond : "" + milliSecond;
                strMilliSecond = milliSecond < 100 ? "0" + strMilliSecond : "" + strMilliSecond;
            } else {
                strDay = day.ToString();
                strHour = hour.ToString();
                strMinute = minute.ToString();
                strSecond = second.ToString();
                strMilliSecond = milliSecond.ToString();
            }
            return strDay + "天" + strHour + "小时" + strMinute + "分" + strSecond + "秒" + strMilliSecond + "毫秒";
        }

        /// <summary>
        /// 获取字符串中的第一个数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>Int型数字</returns>
        public static int GetFirstNumberFromString(string str) {
            int result = 0;
            if (!string.IsNullOrWhiteSpace(str)) {
                Regex reg = new Regex(@"\d+");
                MatchCollection matches = reg.Matches(str);
                if (matches.Count > 0) {
                    str = matches[0].Value;
                    int num = 0;
                    if (int.TryParse(str, out num)) {
                        result = int.Parse(str);
                    } else {
                        result = num;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取Url的文件名函数
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrlFileName(string url) {
            if (string.IsNullOrEmpty(url)) {
                return "";
            }
            string[] strArray = url.Split(new char[] { '/' });
            return strArray[strArray.Length - 1].Split(new char[] { '?' })[0];
        }
    }
}
