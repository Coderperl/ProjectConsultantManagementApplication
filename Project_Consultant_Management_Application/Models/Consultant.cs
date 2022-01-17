using System;
using System.Collections.Generic;

namespace Project_Consultant_Management_Application.Models
{
    public partial class Consultant
    {
        public Consultant()
        {
            Projects = new HashSet<Project>();
        }

        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName =>  FirstName +" "+ LastName;

        public virtual ICollection<Project> Projects { get; set; }
    }
}
