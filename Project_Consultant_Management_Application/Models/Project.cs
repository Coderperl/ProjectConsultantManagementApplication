using System;
using System.Collections.Generic;

namespace Project_Consultant_Management_Application.Models
{
    public partial class Project
    {
        public Project()
        {
            Consultants = new HashSet<Consultant>();
        }

        public long Id { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public long? CompanyId { get; set; }

        public virtual Company? Company { get; set; }

        public virtual ICollection<Consultant> Consultants { get; set; }
    }
}
