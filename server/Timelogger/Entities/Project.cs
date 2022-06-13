using System;
using System.Collections.Generic;

namespace Timelogger.Entities
{
	public class Project
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime Start { get; set; }
        public decimal HourlyRate { get; set; }
        public ICollection<TimeRegistration> TimeRegistrations { get; set; }

        public Project()
        {
            TimeRegistrations = new List<TimeRegistration>();
        }

    }
}
