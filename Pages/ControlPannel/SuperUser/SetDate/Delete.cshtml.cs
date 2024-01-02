using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.SetDate
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public DeleteModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public DateSettings DateSettings { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DateSettings == null)
            {
                return NotFound();
            }

            var datesettings = await _context.DateSettings.FirstOrDefaultAsync(m => m.Id == id);

            if (datesettings == null)
            {
                return NotFound();
            }
            else 
            {
                DateSettings = datesettings;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.DateSettings == null)
            {
                return NotFound();
            }
            var datesettings = await _context.DateSettings.FindAsync(id);

            if (datesettings != null)
            {
                DateSettings = datesettings;
                _context.DateSettings.Remove(DateSettings);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
