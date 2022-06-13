using System;
namespace Timelogger.Api.Models.Project
{
    public class CreateProject
    {
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime Start { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
