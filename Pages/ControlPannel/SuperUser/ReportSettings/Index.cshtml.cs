using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.ReportSettings
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<APP_Report> ReportEntity { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.APP_Reports2 != null)
            {
                ReportEntity = await _context.APP_Reports2.ToListAsync();
            }
        }
    }
}
