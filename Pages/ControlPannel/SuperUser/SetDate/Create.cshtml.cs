using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.SetDate
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public CreateModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DateSettings DateSettings { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.DateSettings == null || DateSettings == null)
            {
                return Page();
            }

            _context.DateSettings.Add(DateSettings);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
