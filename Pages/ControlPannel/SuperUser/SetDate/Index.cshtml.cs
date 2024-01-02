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
    public class IndexModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public IndexModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DateSettings> DateSettings { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DateSettings != null)
            {
                DateSettings = await _context.DateSettings.ToListAsync();
            }
        }
    }
}
