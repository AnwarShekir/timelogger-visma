using System;
namespace Timelogger.Entities
{
    public class TimeRegistration
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid ProjectId { get; set; }
        public int Minutes { get; set; }
    }
}
