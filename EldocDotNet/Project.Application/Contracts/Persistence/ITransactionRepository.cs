﻿using Project.Domain.Entities;

namespace Project.Application.Contracts.Persistence
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
    }
}
