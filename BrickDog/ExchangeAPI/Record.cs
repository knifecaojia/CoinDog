using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeAPI
{
    public enum Period
    {
        PERIOD_M1,
        PERIOD_M5, PERIOD_M15, PERIOD_M30, PERIOD_H1, PERIOD_D1,
    }
    /// <summary>
    /// Order结构里的Status值
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 未完成
        /// </summary>
        ORDER_STATE_PENDING = 1,
        /// <summary>
        /// 已关闭
        /// </summary>
        ORDER_STATE_CLOSED = 2,
        /// <summary>
        /// 已取消

        /// </summary>
        ORDER_STATE_CANCELED = 3,

    }
    /// <summary>
    /// Order结构里的Type值
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 买单
        /// </summary>
        ORDER_TYPE_BUY,
        /// <summary>
        /// 卖单
        /// </summary>
        ORDER_TYPE_SELL,

    }
    /// <summary>
    /// 标准OHLC结构, 用来画K线和指标分析用的，由GetRecords函数返回此结构数组
    /// </summary>
    public class Record
    {
        /// <summary>
        /// 一个时间戳, 精确到毫秒，与Javascript的 new Date().getTime() 得到的结果格式一样
        /// </summary>
        decimal  Time;
        /// <summary>
        /// 开盘价
        /// </summary>
        decimal  Open;

        /// <summary>
        /// 最高价
        /// </summary>
        decimal  High;
        /// <summary>
        /// 最低价
        /// </summary>
        decimal  Low;
        /// <summary>
        /// 收盘价
        /// </summary>
        decimal  Close;
        /// <summary>
        /// 交易量
        /// </summary>
        decimal  Volume;
    }
    /// <summary>
    /// 	市场深度单
    /// </summary>
    public class MarketOrder
    {

        /// <summary>
        /// 价格
        /// </summary>
        decimal  Price;
        /// <summary>
        /// 数量
        /// </summary>
        decimal  Amount;

    }
    /// <summary>
    /// 市场行情
    /// </summary>
    public class Ticker 
    {
        /// <summary>
        /// 最高价
        /// </summary>
        public decimal High { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public decimal Low { get; set; }
        /// <summary>
        /// 卖一价
        /// </summary>
        public decimal Sell { get; set; }
        /// <summary>
        /// 买一价
        /// </summary>

        public decimal Buy { get; set; }
        /// <summary>
        /// 最后成交价
        /// </summary>
        public decimal Last { get; set; }
        /// <summary>
        /// 最近成交量
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// 枚举类型
        /// </summary>

    }
    /// <summary>
    /// 订单结构, 由GetOrder函数返回
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 交易单唯一标识
        /// </summary>
        decimal  Id;
        /// <summary>
        /// 下单价格
        /// </summary>
        decimal  Price;
        /// <summary>
        /// 下单数量
        /// </summary>
        decimal  Amount;
        /// <summary>
        /// 成交数量
        /// </summary>
        decimal  DealAmount;
        /// <summary>
        /// 订单状态, 参考常量里的订单状态
        /// </summary>
        OrderStatus Status;
        /// <summary>
        /// 订单类型, 参考常量里的订单类型
        /// </summary>
        OrderType Type;
    }
    /// <summary>
    /// 市场深度,由GetDepth函数返回
    /// </summary>
    public class Depth
    {
        /// <summary>
        /// 卖单数组, MarketOrder数组, 按价格从低向高排序
        /// </summary>
        List<MarketOrder> marAsks;
        /// <summary>
        /// 买单数组, MarketOrder数组, 按价格从高向低排序
        /// </summary>
        List<MarketOrder> Bids;
    }
    /// <summary>
    /// 获取所有交易历史(非自己),由GetTrades函数返回
    /// </summary>
    public class Trade 
{
        /// <summary>
        /// 时间(Unix timestamp 毫秒)
        /// </summary>
        decimal  Time;
        /// <summary>
        /// 价格
        /// </summary>
        decimal  Price;
        /// <summary>
        /// 数量
        /// </summary>
        decimal  Amount;
        /// <summary>
        /// 订单类型, 参考常量里的订单类型
        /// </summary>
        OrderType Type;
}
    /// <summary>
    /// 手续费结构, 由GetFee函数返回(如国外平台bitfinex买入卖出手续费跟账户交易量相关)
    /// </summary>
    public class Fee
    {
        /// <summary>
        /// 卖出手续费, 为一个浮点数, 如0.2表示0.2 % 的手续费
        /// </summary>
        decimal  Sell;
        /// <summary>
        /// : 买入手续费, 格式同上
        /// </summary>
        decimal  Buy;
    }
    /// <summary>
    /// 账户信息, 由GetAccount函数返回
    /// </summary>
    public  class Account   
{
        /// <summary>
        /// 余额(人民币或者美元, 在Poloniex交易所里BTC_ETC这样的品种, Balance就指的是BTC的数量, Stocks指的是ETC数量, BTC38的ETC_BTC相当于Poloniex的BTC_ETC, 指的是以BTC计价)
        /// </summary>
        decimal  Balance;
        /// <summary>
        /// 冻结的余额
        /// </summary>
        decimal  FrozenBalance;
        /// <summary>
        /// BTC/LTC数量, 现货为当前可操作币的余额(去掉冻结的币), 期货的话为合约当前可用保证金(传统期货为此属性)
        /// </summary>
        decimal  Stocks;
        /// <summary>
        /// 冻结的BTC/LTC数量(传统期货无此属性)
        /// </summary>
        decimal  FrozenStocks;
}
    /// <summary>
    /// 期货交易中的持有仓位信息, 由GetPosition()函数返回此结构数组
    /// </summary>
    public class Position  
{
        /// <summary>
        /// 杆杠大小, 796期货有可能为5, 10, 20三个参数, OKCoin为10或者20, BitVC期货和OK期货的全仓模式返回为固定的10, 因为原生API不支持
        /// </summary>
        decimal  MarginLevel;
        /// <summary>
        /// 持仓量, 796期货表示持币的数量, BitVC指持仓的总金额(100的倍数), OKCoin表示合约的份数(整数且大于1)
        /// </summary>
        decimal  Amount;
        /// <summary>
        /// :可平量, 只有股票有此选项, 表示可以平仓的数量(股票为T+1)今日仓不能平
        /// </summary>
        decimal  CanCover;
        /// <summary>
        /// 仓位冻结量
        /// </summary>
        decimal  FrozenAmount    ;
        /// <summary>
        /// 持仓均价
        /// </summary>
        decimal  Price   ;
        /// <summary>
        /// 持仓浮动盈亏(数据货币单位：BTC/LTC, 传统期货单位:RMB, 股票不支持此字段, 注: OKCoin期货全仓情况下指实现盈余, 并非持仓盈亏, 逐仓下指持仓盈亏)
        /// </summary>
        decimal  Profit;
        /// <summary>
        /// PD_LONG为多头仓位(CTP中用closebuy_today平仓), PD_SHORT为空头仓位(CTP用closesell_today)平仓, (CTP期货中) PD_LONG_YD为咋日多头仓位(用closebuy平), PD_SHORT_YD为咋日空头仓位(用closesell平)
        /// </summary>
        string Type;
        /// <summary>
        /// 商品期货为合约代码, 股票为'交易所代码_股票代码', 具体参数SetContractType的传入类型
        /// </summary>
        string ContractType;	
}
    public enum TickerPair
    {
        btc_cny,
        btc_usd,
        btc_eur,
        ltc_btc,
        ltc_usd,
        cny_usd,
        Unknown
    }
}
