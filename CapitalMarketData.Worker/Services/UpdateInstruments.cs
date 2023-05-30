using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using CapitalMarketData.Entities.Enums;
using CapitalMarketData.Worker.Models;
using Serilog;

namespace CapitalMarketData.Worker.Services;

public class UpdateInstruments
{
    private readonly IEtfRepository _etfRepo;
    private readonly IStockRepository _stockRepo;

    public UpdateInstruments(IEtfRepository etfRepo, IStockRepository stockRepo)
    {
        _etfRepo = etfRepo;
        _stockRepo = stockRepo;
    }

    public async Task Update()
    {
        var insCodes = await TsetmcService.GetInsCodesFromFile();
        foreach (var code in insCodes)
        {
            try
            {
                var data = await TsetmcService.GetIntrumentInfo(code) ?? throw new Exception();

                if (int.Parse(data.instrumentIdentity.sector.cSecVal) == 68 && (int.Parse(data.instrumentIdentity.yVal) == 305 || (int.Parse(data.instrumentIdentity.yVal) == 380)))
                {
                    await UpdateETFs(data, code);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error On {code}: {ex.Message}");
            }
            finally
            {
                await Task.Delay(500);
            }
        }
    }

    private async Task UpdateETFs(InstrumentIdentityRoot data, string code)
    {
        var etf = await _etfRepo.GetById(data.instrumentIdentity.instrumentID);
        if (etf is null)
        {
            ETF newEtf = new()
            {
                Id = data.instrumentIdentity.instrumentID,
                InsCode = code,
                Ticker = data.instrumentIdentity.lVal18AFC,
                Name = data.instrumentIdentity.lVal30,
                Type = int.Parse(data.instrumentIdentity.yVal),
            };

            await _etfRepo.Add(newEtf);
        }
    }

    private async Task UpdateStocks(InstrumentIdentityRoot data, string code)
    {
        var stock = await _stockRepo.GetById(data.instrumentIdentity.instrumentID);
        if (stock is null)
        {
            var newStock = new Entities.Entities.Stock()
            {
                Id = data.instrumentIdentity.instrumentID,
                InsCode = code,
                Ticker = data.instrumentIdentity.lVal18AFC,
                Name = data.instrumentIdentity.lVal30,
                Type = int.Parse(data.instrumentIdentity.yVal),
                //Board = ,
                Industry = (Industry)int.Parse(data.instrumentIdentity.sector.cSecVal),
            };

            await _stockRepo.Add(newStock);
        }
    }
}
