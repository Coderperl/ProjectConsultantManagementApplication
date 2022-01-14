using System;
using System.Collections.Generic;

namespace Project_Consultant_Management_Application.Models
{
    public partial class Company
    {
        public Company()
        {
            Projects = new HashSet<Project>();
        }

        public long Id { get; set; }
        public string? CompanyName { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
