#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_Consultant_Management_Application.Models;

namespace Project_Consultant_Management_Application.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly Project_Consultant_Management_Application.Models.PCMADBContext _context;

        public IndexModel(Project_Consultant_Management_Application.Models.PCMADBContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; }
        
        public async Task OnGetAsync()
        {
            Project = await _context.Projects
                .Include(p => p.Company).
                Include(c => c.Consultants).
                ToListAsync();
        }
    }
}
