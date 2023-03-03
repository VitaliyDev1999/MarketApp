using MarketApp.Domain.Entities;
using MarketApp.Domain.Models;
using MediatR;

namespace MarketApp.Application.Queries;

public record GetMarketDataQuery : IRequest<IEnumerable<CandleStick>>;
