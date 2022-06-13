using System;
using System.Collections.Generic;
using Timelogger.Entities;

namespace Timelogger.Persistence.Contracts
{
    public interface ICompanyRepository
    {
        Entities.Company GetSingle(Guid id);
        List<Entities.Company> GetList();
        void Create(Entities.Company company);
        List<Entities.Company> Find(string query);
    }
}
