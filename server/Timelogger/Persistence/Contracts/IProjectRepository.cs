using System;
using System.Collections.Generic;
using Timelogger.Entities;

namespace Timelogger.Persistence.Contracts
{
    public interface IProjectRepository
    {
        Entities.Project GetSingle(Guid id);
        List<Entities.Project> GetList();
        List<Entities.Project> GetCompanyProjects(Guid companyId);
        void Create(Entities.Project project);
    }
}
