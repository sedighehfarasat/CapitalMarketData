﻿using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Entities.Contracts;

public interface IEtfRepository
{
    Task<List<ETF>> GetAll();
    Task<ETF?> GetById(string id);
    Task<ETF?> GetByTicker(string ticker);
}