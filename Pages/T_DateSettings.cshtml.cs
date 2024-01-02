using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages
{
    public class T_DateSettingsModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext; // Replace with your actual DbContext

        public List<DateSettings> DateSettings { get; set; }

        public T_DateSettingsModel(ApplicationDbContext dbContext) // Replace with your DbContext
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            // Load the data from the database and display it in the Razor Page
            // DateSettings = _dbContext.Table_5.ToList();
            DateSettings = _dbContext.DateSettings.ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return Page();
            }

            // Update the data and save it back to the database
            foreach (var item in DateSettings)
            {
                var entity = _dbContext.DateSettings.Find(item.Id);
                if (entity != null)
                {
                    entity.DateActual = item.DateActual;
                    // You can update other properties here if needed
                }
            }

            _dbContext.SaveChanges();
                       

            return RedirectToPage();


        }
    }
}
