using System;
using System.Collections.Generic;
using Timelogger.Api.Models.Project;

namespace Timelogger.Api.Services.Project
{
    public interface IProjectService
    {
        ProjectDTO GetSingle(Guid id);
        List<ProjectDTO> GetList();
        List<ProjectDTO> GetCompanyProjects(Guid companyId);
        void Create(CreateProject dto);
    }
}
