using System;
namespace Timelogger.Api.Models.TimeRegistration
{
    public class TimeRegistrationDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid ProjectId { get; set; }
        public int Minutes { get; set; }
    }
}
