﻿using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Entities.Contracts;

public interface ITradingDataRepository
{
    Task<TradingData?> GetTodayDataByInstrumentId(string instrumentId);
    Task<int> Add(TradingData? data);
}
