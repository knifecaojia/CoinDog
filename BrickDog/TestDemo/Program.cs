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
            Console.ReadKey();
            while (true)
            {
                ExchangeAPI.exchange exchange = new ExchangeAPI.exchange("btce", "", "");
                ExchangeAPI.Ticker t = exchange.GetTicker(ExchangeAPI.TickerPair.btc_usd);
                Type type = t.GetType();
                PropertyInfo[] PropertyList = type.GetProperties();
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
