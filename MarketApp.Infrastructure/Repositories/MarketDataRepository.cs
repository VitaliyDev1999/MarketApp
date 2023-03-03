using MarketApp.Domain.Entities;
using MarketApp.Domain.Interfaces;
using System.Globalization;

namespace MarketApp.Infrastructure.Repositories;

public class MarketDataRepository : IMarketDataRepository
{
    private const string TIME_FORMAT = "dd/MM/yyyy HH:mm:ss.fff";
    private static char[] DELIMITER = new[] { ',' };

    public async Task<IEnumerable<MarketLine>> GetMarketDataAsync()
    {
        IEnumerable<MarketLine> marketList = (await File.ReadAllLinesAsync("MarketDataTest.csv"))
        .Skip(1)
        .Select(csvLine =>
           {
               string[] arrLine = csvLine.Split(',');
               MarketLine marketLine = new MarketLine(DateTime.ParseExact(arrLine[0], TIME_FORMAT, CultureInfo.InvariantCulture), Int64.Parse(arrLine[1]), decimal.Parse(arrLine[2]));
               return marketLine;
           }).OrderBy(x => x.Time).ToList();

        return marketList;
    }
}
