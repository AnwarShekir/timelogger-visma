using System;
using System.Collections.Generic;
using Timelogger.Entities;

namespace Timelogger.Persistence.Contracts
{
    public interface ITimeRegistrationRepository
    {
        Entities.TimeRegistration GetSingle(Guid id);
        List<Entities.TimeRegistration> GetList();
        List<Entities.TimeRegistration> GetProjectTimeRegistrations(Guid projectId);
        void Create(Entities.TimeRegistration timeRegistration);
    }
}
