using System;
using System.Linq;
using Timelogger.Persistence.Contracts;

namespace Timelogger.Persistence.Company
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly ApiContext _apiContext;

        public CompanyRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public void Create(Entities.Company company)
        {
            _apiContext.Companies.Add(company);
            _apiContext.SaveChanges();
        }

        public System.Collections.Generic.List<Entities.Company> GetList()
        {
            return _apiContext.Companies.ToList();
        }

        public Entities.Company GetSingle(Guid id)
        {
            return _apiContext.Companies.First(s => s.Id == id);
        }
    }
}
