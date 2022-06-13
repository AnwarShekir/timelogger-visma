using System;
using System.Linq;
using Timelogger.Persistence.Contracts;

namespace Timelogger.Persistence.TimeRegistration
{
    public class TimeRegistrationRepository:ITimeRegistrationRepository
    {
        private readonly ApiContext _apiContext;
        public TimeRegistrationRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public void Create(Entities.TimeRegistration timeRegistration)
        {
            _apiContext.TimeRegistrations.Add(timeRegistration);
            _apiContext.SaveChanges();
        }

        public System.Collections.Generic.List<Entities.TimeRegistration> GetList()
        {
            return _apiContext.TimeRegistrations.ToList();
        }

        public System.Collections.Generic.List<Entities.TimeRegistration> GetProjectTimeRegistrations(Guid projectId)
        {
            return _apiContext.TimeRegistrations.Where(s => s.ProjectId == projectId).ToList();
        }

        public Entities.TimeRegistration GetSingle(Guid id)
        {
            return _apiContext.TimeRegistrations.First(s => s.Id == id);
        }
    }
}
