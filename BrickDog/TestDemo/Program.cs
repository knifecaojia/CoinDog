using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace TestDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal hl = ExchangeAPI.ERate.GetHL();
            //Console.ReadKey();
            while (true)
            {
                ExchangeAPI.exchange exchangebtce = new ExchangeAPI.exchange("btce", "", "");
                ExchangeAPI.Ticker t = exchangebtce.GetTicker(ExchangeAPI.TickerPair.btc_usd);
                Type type = t.GetType();
                PropertyInfo[] PropertyList = type.GetProperties();
                Console.Write("BTC-E ticker BTC-USD(CNY)");
                foreach (PropertyInfo item in PropertyList)
                {
                    string name = item.Name;
                    object value = item.GetValue(t, null);
                    decimal v = 0;
                    decimal.TryParse(value.ToString(), out v);
                    v = v * hl / 100;
                    if (v < 200000)
                        Console.Write(name + ":" + v.ToString() + "  ");
                    else
                        Console.Write(name + ":" + value.ToString() + "  ");
                }
                Console.Write("\r\n");
                ExchangeAPI.exchange exchangeokc = new ExchangeAPI.exchange("okcn", "", "");
                t = exchangeokc.GetTicker(ExchangeAPI.TickerPair.btc_cny);
                type = t.GetType();
                PropertyList = type.GetProperties();
                Console.Write("OKcoinCN ticker BTC-CNY");
                foreach (PropertyInfo item in PropertyList)
                {
                    string name = item.Name;
                    object value = item.GetValue(t, null);
                    Console.Write(name + ":" + value.ToString() + "  ");
                }
                Console.Write("\r\n");
                Thread.Sleep(10 * 1000);
            }
        }
    }
}
