﻿using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Entities.Contracts;

public interface IStockRepository
{
    Task<List<Stock>> GetAll();
    Task<Stock?> GetById(string id);
    Task<Stock?> GetByTicker(string ticker);
}