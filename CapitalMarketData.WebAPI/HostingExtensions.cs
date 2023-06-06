using CapitalMarketData.Data;
using CapitalMarketData.Data.Repositories;
using CapitalMarketData.Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CapitalMarketData.WebApi;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found.");
        builder.Services.AddDbContext<CapitalMarketDataDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IInstrumentRepository, InstrumentRepository>();
        builder.Services.AddScoped<ITradingDataRepository, TradingDataRepository>();
        builder.Services.AddScoped<IIndiInstiTradingDataRepository, IndiInstiTradingDataRepository>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            // Generates HTML error responses
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            // Adds a middleware to the pipeline that will catch exceptions, log them, and re-execute the request in an alternate pipeline.
            app.UseExceptionHandler("/Home/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
