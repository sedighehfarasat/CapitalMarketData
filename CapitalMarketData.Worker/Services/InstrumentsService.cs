﻿using CapitalMarketData.Entities.Contracts;
using CapitalMarketData.Entities.Entities;
using CapitalMarketData.Entities.Enums;
using CapitalMarketData.Worker.Models;

namespace CapitalMarketData.Worker.Services;

public class InstrumentsService
{
    private readonly IInstrumentRepository _instrumentRepo;
    private readonly IEtfRepository _etfRepo;
    private readonly IStockRepository _stockRepo;

    public InstrumentsService(IInstrumentRepository instrumentRepo, IEtfRepository etfRepo, IStockRepository stockRepo)
    {
        _instrumentRepo = instrumentRepo;
        _etfRepo = etfRepo;
        _stockRepo = stockRepo;
    }

    public async Task Update()
    {
        var insCodes = await TsetmcService.GetInsCodes();
        foreach (var code in insCodes)
        {
            var data = await TsetmcService.GetIntrumentInfo(code);
            if (data is null) continue;

            switch (Enum.Parse(typeof(InstrumentType), data.instrumentIdentity.yVal))
            {
                case InstrumentType.Etf:
                case InstrumentType.CommodityEtf:
                    await UpdateETFs(data, code);
                    break;
                case InstrumentType.Stock_Exchange:
                case InstrumentType.Stock_OffExchange:
                case InstrumentType.Stock_BaseMarket:
                case InstrumentType.Stock4:
                    await UpdateStocks(data, code);
                    break;
                default:
                    break;
            }

            await Task.Delay(500);
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
                Type = (InstrumentType)int.Parse(data.instrumentIdentity.yVal),
                Sector = (Entities.Enums.Sector)int.Parse(data.instrumentIdentity.sector.cSecVal),
                Subsector = (Entities.Enums.Subsector)data.instrumentIdentity.subSector.cSoSecVal,
            };

            await _instrumentRepo.Add(newEtf);
        }
    }

    private async Task UpdateStocks(InstrumentIdentityRoot data, string code)
    {
        var stock = await _stockRepo.GetById(data.instrumentIdentity.instrumentID);
        if (stock is null)
        {
            Entities.Entities.Stock newStock = new()
            {
                Id = data.instrumentIdentity.instrumentID,
                InsCode = code,
                Ticker = data.instrumentIdentity.lVal18AFC,
                Name = data.instrumentIdentity.lVal30,
                Type = (InstrumentType)int.Parse(data.instrumentIdentity.yVal),
                Sector = (Entities.Enums.Sector)int.Parse(data.instrumentIdentity.sector.cSecVal),
                Subsector = (Entities.Enums.Subsector)data.instrumentIdentity.subSector.cSoSecVal,
            };

            await _instrumentRepo.Add(newStock);
        }
    }
}
