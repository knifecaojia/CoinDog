using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Data;
namespace TestAPI
{
    class Program
    {
        static string Furl = "https://www.okex.com/api/v1/future_depth.do?symbol=ltc_usd&contract_type=this_week&size=1";
        static string Surl = "https://www.okcoin.cn/api/v1/depth.do?symbol=ltc_cny&size=1";
        static string Durl = "https://www.okex.com/api/v1/exchange_rate.do";
        static decimal rate = 1;//汇率
        static LogClass log;
        static DataTable dt;
        static void Main(string[] args)
        {
            int index = 0;
            log = new LogClass();
            dt = new DataTable();
            DataColumn dc1 = new DataColumn("timestamp", typeof(DateTime));
            DataColumn dc2 = new DataColumn("fask1", typeof(decimal));
            DataColumn dc3 = new DataColumn("fask1amountltc", typeof(decimal));
            DataColumn dc4 = new DataColumn("fbid1", typeof(decimal));
            DataColumn dc5 = new DataColumn("fbid1amountltc", typeof(decimal));
            DataColumn dc6 = new DataColumn("sask1", typeof(decimal));
            DataColumn dc7 = new DataColumn("sask1amount", typeof(decimal));
            DataColumn dc8 = new DataColumn("sbid1", typeof(decimal));
            DataColumn dc9 = new DataColumn("sbid1amount", typeof(decimal));
            DataColumn dc10 = new DataColumn("diff", typeof(decimal));
            DataColumn dc11 = new DataColumn("mindiff", typeof(decimal));
            DataColumn dc12 = new DataColumn("maxdiff", typeof(decimal));
            DataColumn dc13 = new DataColumn("avadiff", typeof(decimal));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
            dt.Columns.Add(dc9);
            dt.Columns.Add(dc10);
            dt.Columns.Add(dc11);
            dt.Columns.Add(dc12);
            dt.Columns.Add(dc13);
            int runtimes = 0;
            decimal diff, mindiff = 1000, maxdiff = -1000, avadiff = 0, sum = 0;
            int max = 1000;
            Console.WriteLine("input rows you want to get! default 1000 ");
            while (!int.TryParse(Console.ReadLine(), out max))
            {
                Console.WriteLine("input is not integer.try angin");
                Console.WriteLine("input rows you want to get! default 1000 ");
            }
            Console.WriteLine("Go for records:"+max.ToString());
            while (index< max)
            {
                try
                {
                   
                   

                    string rate_str = GetHttpsResponseStr(Durl);
                    decimal.TryParse(JObject.Parse(rate_str)["rate"].ToString(), out rate);
                    decimal fask1, fask1amount, fask1amountltc, fbid1, fbid1amount, fbid1amountltc, sask1, sask1amount, sbid1, sbid1amount;
                    JObject fjo = JObject.Parse(GetHttpsResponseStr(Furl));
                    decimal.TryParse(fjo["asks"][0][0].ToString(), out fask1);
                    decimal.TryParse(fjo["asks"][0][1].ToString(), out fask1amount);
                    decimal.TryParse(fjo["bids"][0][0].ToString(), out fbid1);
                    decimal.TryParse(fjo["bids"][0][1].ToString(), out fbid1amount);

                    fask1amountltc = fask1amount * 10 / fask1;
                    fbid1amountltc = fbid1amount * 10 / fbid1;
                    fask1 = fask1 * rate;
                    fbid1 = fbid1 * rate;

                    JObject sjo = JObject.Parse(GetHttpsResponseStr(Surl));
                    decimal.TryParse(sjo["asks"][0][0].ToString(), out sask1);
                    decimal.TryParse(sjo["asks"][0][1].ToString(), out sask1amount);
                    decimal.TryParse(sjo["bids"][0][0].ToString(), out sbid1);
                    decimal.TryParse(sjo["bids"][0][1].ToString(), out sbid1amount);

                    diff = fask1 - sbid1;
                    if (diff < mindiff)
                        mindiff = diff;
                    if (diff > maxdiff)
                        maxdiff = diff;
                    sum += diff;


                    runtimes++;
                    avadiff = sum / runtimes;
                    string str = "期现差: " + diff.ToString("#0.00") + "期买一: " + fask1.ToString("#0.00") + "量-ltc:" + fask1amount.ToString("#0.0") + "-" + fask1amountltc.ToString("#0.00") + "卖一: " + fbid1.ToString("#0.00") + "量-ltc" + fbid1amount.ToString("#0.00") + "-" + fbid1amountltc.ToString("#0.00") + " 现货卖一: " + sbid1.ToString("#0.00") + "量: " + sbid1amount.ToString("#0.0") + "买一: " + sask1.ToString("#0.00") + "量" + sask1amount.ToString("#0.00") + "min:" + mindiff.ToString("#0.00") + "max:" + maxdiff.ToString("#0.00") + "avg:" + avadiff.ToString("#0.00");
                    Console.WriteLine(str);

                    DataRow dr = dt.NewRow();

                    dr[0] = DateTime.Now;
                    dr[1] = fask1;
                    dr[2] = fask1amountltc;
                    dr[3] = fbid1;
                    dr[4] = fbid1amountltc;
                    dr[5] = sask1;
                    dr[6] = sask1amount;
                    dr[7] = sbid1;
                    dr[8] = sbid1amount;
                    dr[9] = diff;
                    dr[10] = mindiff;
                    dr[11] = maxdiff;
                    dr[12] = avadiff;
                    dt.Rows.Add(dr);
                    //log.WriteLogFile(str);
                    index++;
                    Thread.Sleep(5000);

                   
                }
                catch(Exception e)
                {
                    log.WriteLogFile(e.Message+e.Source);
                }
            }
            dataTableToCsv(dt, "LogTable"+DateTime.Now.ToLongDateString()+ DateTime.Now.ToLongTimeString().Replace(":","") + ".csv");

        }
        /// <summary>
        /// 导出文件为csv
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="file">导出路径</param>
        public static void dataTableToCsv(DataTable table, string file)
        {
            string title = "";
            FileStream fs = new FileStream(file, FileMode.Create);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), Encoding.Default);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                title += table.Columns[i].ColumnName + ",";
            }
            title = title.Substring(0, title.Length - 1) + "\n";
            sw.Write(title);
            foreach (DataRow row in table.Rows)
            {
                string line = "";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    line += row[i].ToString() + ",";
                }
                line = line.Substring(0, line.Length - 1) + "\n";
                sw.Write(line);
            }
            sw.Close();
            fs.Close();
        }
        public static string GetHttpsResponseStr(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            string str = StreamToString(resStream);
            return str;
        }
        public static string StreamToString(Stream stream)
        {
           // stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
