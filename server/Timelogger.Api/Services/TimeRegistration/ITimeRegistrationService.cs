using System;
using System.Collections.Generic;
using Timelogger.Api.Models.TimeRegistration;

namespace Timelogger.Api.Services.TimeRegistration
{
    public interface ITimeRegistrationService
    {
        TimeRegistrationDTO GetSingle(Guid id);
        List<TimeRegistrationDTO> GetList();
        List<TimeRegistrationDTO> GetProjectTimeRegistrations(Guid projectId);
        void Create(CreateTimeRegistration dto);
    }
}
