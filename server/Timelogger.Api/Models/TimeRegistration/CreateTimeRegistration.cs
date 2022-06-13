using System;
namespace Timelogger.Api.Models.TimeRegistration
{
    public class CreateTimeRegistration
    {
        public DateTime Date { get; set; }
        public Guid ProjectId { get; set; }
        public int Minutes { get; set; }
    }
}
