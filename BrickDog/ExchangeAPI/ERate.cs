using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExchangeAPI
{
    public class ERate
    {
        static public decimal GetHL(ExchangeRatePair erp=ExchangeRatePair.cny_usd)
        {
            decimal hld = 1;//默认汇率为1
            string hl = null;
            try
            {
                string tempurl = "http://www.boc.cn/sourcedb/whpj/index.html";
                HttpWebRequest webr = (HttpWebRequest)WebRequest.Create(tempurl);//创建请求
                HttpWebResponse wb = (HttpWebResponse)webr.GetResponse();
                Stream sr = wb.GetResponseStream();//得到返回数据流
                StreamReader sr1 = new StreamReader(sr, Encoding.GetEncoding("utf-8"));//用于读取数据流的内容
                string zz = sr1.ReadToEnd();//读取完成
                sr1.Close();
                wb.Close();//关闭

                string temp = "";
                switch (erp)
                {
                    case ExchangeRatePair.cny_eu:
                        temp = @"<tr>\s*<td>欧元</td>([\s\S]*?)</tr>"; ;
                        break;
                    case ExchangeRatePair.cny_usd:
                        temp = @"<tr>\s*<td>美元</td>([\s\S]*?)</tr>";
                        break;
                    case ExchangeRatePair.cny_jp:
                        temp = @"<tr>\s*<td>日元</td>([\s\S]*?)</tr>"; ;
                        break;
                    default:
                        break;
                }
                hl = Regex.Match(zz, temp, RegexOptions.IgnoreCase).Value;
                Regex regex = new Regex(@"<td>([\s\S]*?)</td>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (regex.IsMatch(hl))
                {
                    MatchCollection matchCollection = regex.Matches(hl);
                    if (matchCollection.Count > 5)
                    {
                        decimal.TryParse(matchCollection[5].ToString().Replace("<td>", "").Replace("</td>", ""), out hld);
                    }
                }

                
                return hld;
            }
            catch { return 1; }
        }
        public enum ExchangeRatePair
        {
            cny_usd,
            cny_eu,
            cny_jp,
        }
    }
}
