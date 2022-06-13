using System;
using System.Linq;
using Timelogger.Persistence.Contracts;

namespace Timelogger.Persistence.Project
{
    public class ProjectRepository:IProjectRepository
    {
        private readonly ApiContext _apiContext;
        public ProjectRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public void Create(Entities.Project project)
        {
            _apiContext.Projects.Add(project);
            _apiContext.SaveChanges();
        }

        public System.Collections.Generic.List<Entities.Project> GetCompanyProjects(Guid companyId)
        {
            return _apiContext.Projects.Where(s => s.CompanyId == companyId).ToList();
        }

        public System.Collections.Generic.List<Entities.Project> GetList()
        {
            return _apiContext.Projects.ToList();
        }

        public Entities.Project GetSingle(Guid id)
        {
            return _apiContext.Projects.First(s => s.Id == id);
        }
    }
}
