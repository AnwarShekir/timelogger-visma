using System;
using System.Collections.Generic;
using Timelogger.Api.Models.Company;

namespace Timelogger.Api.Services.Company
{
    public interface ICompanyService
    {
        CompanyDTO GetSingle(Guid id);
        List<CompanyDTO> GetList();
        void Create(CreateCompany dto);
    }
}
