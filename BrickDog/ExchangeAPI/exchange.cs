using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeAPI
{
    public class exchange
    {
        string APIkey;
        string APISecret;
        string MarketName;

        public exchange(string marketname, string ak, string asect)
        {
            MarketName = marketname;
            APIkey = ak;
            APISecret = asect;
        }


        #region Public method 
        /// <summary>
        /// 根据当前市场实例，返回市场行情
        /// </summary>
        /// <returns></returns>
        public Ticker GetTicker(TickerPair tp=TickerPair.btc_cny)
        {
            Ticker ticker = new Ticker();
            switch (MarketName)
            {
                case "btce":
                    ticker = MarketConvert.ConvertTicker(BtcE.BtceApi.GetTicker(BtcE.BtcePairHelper.FromString(tp.ToString()))) ;
                    break;

            }
            
            return ticker;
        }
        /// <summary>
        /// 根据市场实例，返回市场深度
        /// </summary>
        /// <returns></returns>
        public Depth GetDepth()
        {
            Depth depth = new Depth();
            return depth;
        }
        public List<Trade> GetTrades()
        {
            List<Trade> trades = new List<Trade>();
            return trades;
        }
        /// <summary>
        /// 不加参数, 默认返回添加机器人时时指量的K线周期, 但也可以自定义K线周期
        ///支持: PERIOD_M1 指1分钟, PERIOD_M5 指5分钟, PERIOD_M15 指15分钟, PERIOD_M30 指30分钟, PERIOD_H1 指1小时, PERIOD_D1 指一天
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Record> GetRecords(Period p)
        {
            List<Record> records = new List<Record>();
            return records;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Account GetAccount()
        {
            Account account = new Account();
            return account;
        }
        /// <summary>
        /// 可以跟多余的参数做为附加消息显示到日志, 如exchange.Buy(1000,0.1, "OK", 123)
        ///支持现货(火币/BitVC/OKCoin/OKCoin国际/OKCoin期货/BTCChina/BitYes)市价单, 市价单价格指定为-1
        ///exchange.Buy(1000), 指买市价1000元的币, BTCChina例外exchange.Buy(0.3)指市价买0.3个币
        /// </summary>
        /// <param name="price"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string Buy(double price, double amount)
        {
            return "0";
        }
        /// <summary>
        /// 	跟Buy函数一样的调用方法和场景
        /// </summary>
        /// <param name="price"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string Sell(double price, double amount)
        {
            return "0";
        }
        /// <summary>
        /// 获取所有未完成的订单, 返回一个Order数组结构
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            return orders;
        }
        /// <summary>
        /// 根据订单号获取订单详情, 返回一个Order结构
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public Order GetOrder(string orderid)
        {
            Order order = new Order();
            return order;
        }
        /// <summary>
        /// 根据订单号取消一个订单, 返回true或者false
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public bool CancelOrder(string orderid)
        {
            return false;
        }
        public Fee GetFee()
        {
            return new Fee();
        }
        #endregion
        private bool IsAppKeyRequired()
        {
            switch (MarketName)
            {
                case "btce":
                    return true;
                case "huobi":
                    return true;
                default:
                    return false;
            }
        }
    }
}
