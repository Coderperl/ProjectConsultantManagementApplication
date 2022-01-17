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
    public class DetailsModel : PageModel
    {
        private readonly Project_Consultant_Management_Application.Models.PCMADBContext _context;

        public DetailsModel(Project_Consultant_Management_Application.Models.PCMADBContext context)
        {
            _context = context;
        }

        public Project Project { get; set; }
        [BindProperty]
        public List<Project> Projects { get; set; }
        [BindProperty]
        public Consultant Consultant { get; set; }
        [BindProperty]
        public long[] Consultants { get; set; }

        public IActionResult OnGet(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project =  _context.Projects
                .Include(p => p.Company)
                .Include(p => p.Consultants).

                FirstOrDefault(m => m.Id == id);
            //Consultant = _context.Consultants.Where(c => Consultants.).ToList();

           

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
