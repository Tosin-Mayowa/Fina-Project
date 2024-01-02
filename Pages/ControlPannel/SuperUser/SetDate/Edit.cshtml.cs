using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.SetDate
{
    public class EditModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public EditModel(WebApplication2.Data.ApplicationDbContext context)
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

            var datesettings =  await _context.DateSettings.FirstOrDefaultAsync(m => m.Id == id);
            if (datesettings == null)
            {
                return NotFound();
            }
            DateSettings = datesettings;
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

            _context.Attach(DateSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateSettingsExists(DateSettings.Id))
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

        private bool DateSettingsExists(int id)
        {
          return (_context.DateSettings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
