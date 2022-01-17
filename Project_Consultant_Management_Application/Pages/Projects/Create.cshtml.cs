#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Consultant_Management_Application.Models;

namespace Project_Consultant_Management_Application.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly Project_Consultant_Management_Application.Models.PCMADBContext _context;

        public CreateModel(Project_Consultant_Management_Application.Models.PCMADBContext context)
        {
            _context = context;
        }

        public SelectList CompanyList { get; set; }
        public SelectList ConsultantList { get; set; }


        [BindProperty]
        public Project Project { get; set; }
        [BindProperty]
        public int[] Consultants { get; set; }

        public IActionResult OnGet()
        {
            CompanyList = new SelectList(_context.Companies, "Id", "CompanyName");
            ConsultantList = new SelectList(_context.Consultants, "Id", "FullName");
            return Page();
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            foreach (int id in Consultants)
            {
                Consultant c = _context.Consultants.Single(c => c.Id == id);
                Project.Consultants.Add(c);
            }
            _context.Projects.Add(Project);
             _context.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}
