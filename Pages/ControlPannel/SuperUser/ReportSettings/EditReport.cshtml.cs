using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.ReportSettings
{
    public class EditReportModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public EditReportModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public APP_Report APP_Report { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _context.APP_Reports2 == null)
            {
                return NotFound();
            }

            var app_report = await _context.APP_Reports2.FirstOrDefaultAsync(m => m.Report_ID == id);
            if (app_report == null)
            {
                return NotFound();
            }
            APP_Report = app_report;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(APP_Report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APP_ReportExists(APP_Report.NO))
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

        private bool APP_ReportExists(int id)
        {
            return (_context.APP_Reports2?.Any(e => e.NO == id)).GetValueOrDefault();
        }
    }
}
