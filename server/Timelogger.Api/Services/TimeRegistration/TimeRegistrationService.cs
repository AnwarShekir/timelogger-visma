using System;
using System.Collections.Generic;
using Timelogger.Api.Models.TimeRegistration;
using Timelogger.Persistence.Contracts;

namespace Timelogger.Api.Services.TimeRegistration.cs
{
    public class TimeRegistrationService:ITimeRegistrationService
    {
        private readonly ITimeRegistrationRepository _timeRegistrationsRepository;
        private readonly IProjectRepository _projectRepository;
        public TimeRegistrationService(ITimeRegistrationRepository timeRegistrationRepository,IProjectRepository projectRepository)
        {
            _timeRegistrationsRepository = timeRegistrationRepository;
            _projectRepository = projectRepository;
        }

        public void Create(CreateTimeRegistration dto)
        {
            var project = _projectRepository.GetSingle(dto.ProjectId); 

            //normally would  have a status on the project table to determine which state it is in (completed,ongoing,...)
            //but we use the date here for simplicity
            int validateDate = DateTime.Compare(dto.Date, project.Deadline);
            if(validateDate > 0)
            {
                throw new ArgumentException("Project already completed");
            }

            var entity = new Entities.TimeRegistration()
            {
                Date = dto.Date,
                Id = Guid.NewGuid(),
                Minutes = dto.Minutes,
                ProjectId = dto.ProjectId
            };


            _timeRegistrationsRepository.Create(entity);
            
        }

        public List<TimeRegistrationDTO> GetList()
        {
            var dbEntities = _timeRegistrationsRepository.GetList();
            var result = new List<TimeRegistrationDTO>();
            foreach(var element in dbEntities)
            {
                result.Add(new TimeRegistrationDTO()
                {
                    Date = element.Date,
                    Id = element.Id,
                    Minutes = element.Minutes,
                    ProjectId = element.ProjectId
                });
            }
            return result;
        }

        public List<TimeRegistrationDTO> GetProjectTimeRegistrations(Guid projectId)
        {
            var dbEntities = _timeRegistrationsRepository.GetProjectTimeRegistrations(projectId);
            var result = new List<TimeRegistrationDTO>();
            foreach (var element in dbEntities)
            {
                result.Add(new TimeRegistrationDTO()
                {
                    Date = element.Date,
                    Id = element.Id,
                    Minutes = element.Minutes,
                    ProjectId = element.ProjectId
                });
            }
            return result;
        }

        public TimeRegistrationDTO GetSingle(Guid id)
        {
            var entitty = _timeRegistrationsRepository.GetSingle(id);
            var result = new TimeRegistrationDTO()
            {
                Date = entitty.Date,
                Id = entitty.Id,
                Minutes = entitty.Minutes,
                ProjectId = entitty.ProjectId
            };

            return result;
        }
    }
}
