using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeAPI
{
    class MarketConvert
    {
        public static Ticker ConvertTicker(BtcE.Ticker t)
        {
            Ticker ticker = new Ticker();
            ticker.High = t.High;
            ticker.Low = t.Low;
            ticker.Last = t.Last;
            ticker.Sell = t.Sell;
            ticker.Buy = t.Buy;
            ticker.Volume = t.Volume;
            return ticker;
        }
        public static Ticker ConvertTicker(OKCoinAPI.Ticker t)
        {
            Ticker ticker = new Ticker();
            ticker.High = t.High;
            ticker.Low = t.Low;
            ticker.Last = t.Last;
            ticker.Sell = t.Sell;
            ticker.Buy = t.Buy;
            ticker.Volume = t.Volume;
            return ticker;
        }
        public static OKCoinAPI.TickerType ConvertTickType(TickerPair tp)
        {
            switch (tp)
            {
                case TickerPair.btc_cny:return OKCoinAPI.TickerType.BTC;
                case TickerPair.ltc_cny:return OKCoinAPI.TickerType.LTC;
                case TickerPair.eth_cny:return OKCoinAPI.TickerType.ETH;
                default:return OKCoinAPI.TickerType.BTC;
            }
        }
    }
}
