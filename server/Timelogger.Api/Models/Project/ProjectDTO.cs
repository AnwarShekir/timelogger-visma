using System;
namespace Timelogger.Api.Models.Project
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime Start { get; set; }
        public decimal HourlyRate { get; set; }
        public Guid CompanyId { get; set; }

    }
}
