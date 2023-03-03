using MarketApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Domain.Interfaces;

public interface IMarketDataRepository
{
    Task<IEnumerable<MarketLine>> GetMarketDataAsync();
}
