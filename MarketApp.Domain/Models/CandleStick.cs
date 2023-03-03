using System.Data;

namespace MarketApp.Domain.Models;

public class CandleStick
{
    public decimal Open { get; set; }
    public decimal Close { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal SumVolume { get; set; }
    public DateTime Time { get; set; }
}
