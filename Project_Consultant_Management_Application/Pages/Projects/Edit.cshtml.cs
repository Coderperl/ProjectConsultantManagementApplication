#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Consultant_Management_Application.Models;

namespace Project_Consultant_Management_Application.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly Project_Consultant_Management_Application.Models.PCMADBContext _context;

        public EditModel(Project_Consultant_Management_Application.Models.PCMADBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }
        public SelectList ConsultantList { get; set; }
        [BindProperty]
        public int[] Consultants { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects
                .Include(p => p.Company).FirstOrDefaultAsync(m => m.Id == id);

            if (Project == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName");
            ConsultantList = new SelectList(_context.Consultants, "Id", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            _context.Attach(Project).State = EntityState.Added;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Entry(Project).Collection(p => p.Consultants).Load();
            List<Consultant> ConsultantToAdd = new();
            foreach (int id in Consultants)
            {
                Consultant c = _context.Consultants.Single(c => c.Id == id);
                ConsultantToAdd.Add(c);
            }
            Project.Consultants = ConsultantToAdd;
            _context.Update(Project);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Project.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProjectExists(long id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
