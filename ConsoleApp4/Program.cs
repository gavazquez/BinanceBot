using Binance.Net.Clients;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;

namespace BinanceBot;

public class MainClass
{
    public string ApiKey { get; set; } = "APIKEY";
    public string ApiSecret { get; set; } = "APISECRET";

    public static async Task Main(string[] args)
    {
        await new MainClass().Run();
    }

    public async Task Run()
    {
        BinanceClient.SetDefaultOptions(new BinanceClientOptions()
        {
            ApiCredentials = new ApiCredentials(ApiKey, ApiSecret),
            LogLevel = LogLevel.Debug
        });

        using (var client = new BinanceClient())
        {
            //var orderData = await client.SpotApi.Trading.PlaceOrderAsync(
            //    "BTCUSDT",
            //    OrderSide.Buy,
            //    SpotOrderType.Limit,
            //    0.001m,
            //    50000,
            //    timeInForce: TimeInForce.GoodTillCanceled);

            var bookPrc = await client.SpotApi.ExchangeData.GetBookPriceAsync("BTCUSDT");
            if (bookPrc.Success)
            {
                var bestAskPrc = bookPrc.Data.BestAskPrice;
                Console.WriteLine($"Best ASK price: {bestAskPrc}");
            }
        }
    }
}