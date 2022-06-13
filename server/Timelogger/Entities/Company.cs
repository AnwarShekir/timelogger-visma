using System;
using System.Collections.Generic;

namespace Timelogger.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Project> Projects { get; set; }

        public Company()
        {
            Projects = new List<Project>();
        }
    }
}
