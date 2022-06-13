using System;
using System.Collections.Generic;
using Timelogger.Api.Models.Company;
using Timelogger.Persistence.Contracts;

namespace Timelogger.Api.Services.Company
{
    public class CompanyService:ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        //In a real application, would use automapper or extenstion for mapning. 
        public void Create(CreateCompany dto)
        {
            var company = new Entities.Company
            {
                Address = dto.Address,
                Name = dto.Name,
                Id = Guid.NewGuid()
            };
            _companyRepository.Create(company);
        }

        public List<CompanyDTO> Find(string query)
        {
            List<Entities.Company> dbResult = _companyRepository.Find(query);
            var result = new List<CompanyDTO>();
            foreach (var element in dbResult)
            {
                result.Add(new CompanyDTO()
                {
                    Address = element.Address,
                    Id = element.Id,
                    Name = element.Name
                }
                );
            }
            return result;

        }

        public List<CompanyDTO> GetList()
        {
            List<Entities.Company> dbResult = _companyRepository.GetList();
            var result = new List<CompanyDTO>();
            foreach(var element in dbResult)
            {
                result.Add(new CompanyDTO()
                {
                    Address = element.Address,
                    Id = element.Id,
                    Name = element.Name
                }
                );
            }
            return result;
        }

        public CompanyDTO GetSingle(Guid id)
        {
            Entities.Company entity = _companyRepository.GetSingle(id);
            var result = new CompanyDTO()
            {
                Address = entity.Address,
                Id = entity.Id,
                Name = entity.Name
            };

            return result;
        }
    }
}
