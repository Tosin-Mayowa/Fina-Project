using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.ReportSettings
{
    public class AddReportModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public AddReportModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public APP_Report APP_Report { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.APP_Reports2 == null || APP_Report == null)
            {
                return Page();
            }

            _context.APP_Reports2.Add(APP_Report);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
