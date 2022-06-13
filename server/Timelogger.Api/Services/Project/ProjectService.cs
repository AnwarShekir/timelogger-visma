using System;
using System.Collections.Generic;
using Timelogger.Api.Models.Project;
using Timelogger.Persistence.Contracts;

namespace Timelogger.Api.Services.Project
{
    public class ProjectService:IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICompanyRepository _comppanyRepository;
        public ProjectService(IProjectRepository projectRepository,ICompanyRepository companyRepository)
        {
            _projectRepository = projectRepository;
            _comppanyRepository = companyRepository;
        }
        //stopped using EF a while ago, so forgot how to make foreign keys, but normmaly i would let the database handle if foreign key does not excist.
        public void Create(CreateProject dto)
        {
             _comppanyRepository.GetSingle(dto.CompanyId); //throws error if company does not excist.

            int dateValidateResult = DateTime.Compare(dto.Deadline, dto.Start);
            if(dateValidateResult < 0)
            {
                throw new ArgumentException("Start date can't be after deadline");
            }
            var entity = new Entities.Project()
            {
                CompanyId = dto.CompanyId,
                Deadline = dto.Deadline,
                HourlyRate = dto.HourlyRate,
                Name = dto.Name,
                Start = dto.Start,
                Id = Guid.NewGuid()
            };
            _projectRepository.Create(entity);
        }

        public List<ProjectDTO> GetCompanyProjects(Guid companyId)
        {
            var dbEntities = _projectRepository.GetCompanyProjects(companyId);
            var result = new List<ProjectDTO>();
            foreach(var element in dbEntities)
            {
                result.Add(new ProjectDTO()
                {
                    CompanyId = element.CompanyId,
                    Deadline = element.Deadline,
                    HourlyRate = element.HourlyRate,
                    Id = element.Id,
                    Name = element.Name,
                    Start = element.Start
                });
            }

            return result;
        }

        public List<ProjectDTO> GetList()
        {
            var dbEntities = _projectRepository.GetList();
            var result = new List<ProjectDTO>();
            foreach (var element in dbEntities)
            {
                result.Add(new ProjectDTO()
                {
                    CompanyId = element.CompanyId,
                    Deadline = element.Deadline,
                    HourlyRate = element.HourlyRate,
                    Id = element.Id,
                    Name = element.Name,
                    Start = element.Start
                });
            }

            return result;
        }

        public ProjectDTO GetSingle(Guid id)
        {
            var entity = _projectRepository.GetSingle(id);
            var result = new ProjectDTO()
            {
                CompanyId = entity.CompanyId,
                Deadline = entity.Deadline,
                HourlyRate = entity.HourlyRate,
                Id = entity.Id,
                Name = entity.Name,
                Start = entity.Start
            };
            return result;
        }
    }
}
