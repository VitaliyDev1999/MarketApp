using MediatR;
using MarketApp.Domain.Interfaces;
using MarketApp.Domain.Entities;
using MarketApp.Domain.Models;
using System.Diagnostics;

namespace MarketApp.Application.Queries;

public class GetMarketDataHandler : IRequestHandler<GetMarketDataQuery, IEnumerable<CandleStick>>
{
    private readonly IMarketDataRepository _repository;

    public GetMarketDataHandler(IMarketDataRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CandleStick>> Handle(GetMarketDataQuery request, CancellationToken cancellationToken)
    {
        var markedDataList = await _repository.GetMarketDataAsync();
        if(!markedDataList.Any())
        {
            return Enumerable.Empty<CandleStick>();
        }

        var candleSticks = new List<CandleStick>();

        foreach (var item in markedDataList)
        {
            var candleStick = candleSticks.LastOrDefault();
            if (candleStick == null || item.Time.Minute != candleStick.Time.Minute)
            {
                candleStick = new CandleStick
                {
                    Time = item.Time,
                    Open = item.Price,
                    Close = item.Price,
                    High = item.Price,
                    Low = item.Price,
                    SumVolume = item.Quantity
                };

                candleSticks.Add(candleStick);
            }
            else
            {
                candleStick.Close = item.Price;
                candleStick.High = Math.Max(candleStick.High, item.Price);
                candleStick.Low = Math.Min(candleStick.Low, item.Price);
                candleStick.SumVolume += item.Quantity;
            }
        }
        return candleSticks;
    }
}