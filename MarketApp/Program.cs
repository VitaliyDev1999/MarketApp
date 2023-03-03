using MarketApp.Domain.Interfaces;
using MarketApp.Infrastructure.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var presentationAssembly = typeof(MarketApp.Presentation.AssemblyReference).Assembly;
var applicationAssembly = typeof(MarketApp.Application.AssemblyReference).Assembly;

builder.Services.AddControllers().AddApplicationPart(presentationAssembly);
builder.Services.AddMediatR(applicationAssembly);

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMarketDataRepository, MarketDataRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }