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

        [BindProperty]
        public Project Project { get; set; }
        [BindProperty]
        public List<Project> Projects { get; set; }

        public IActionResult OnGet()
        {
            Projects = _context.Projects
                .Include(p => p.Company).
                Include(c => c.Consultants).
                ToList();

            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName");
            return Page();
            ViewData["Consultant"] = new SelectList(_context.Consultants, "Id", "Consultant");
            return Page();


        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Projects.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
