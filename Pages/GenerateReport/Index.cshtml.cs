using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.Model;
using WebApplication2.Repository;

namespace WebApplication2.Pages.GenerateReport
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
       
        public List<string> Category { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _context = db;
            

        }

        public void OnGet()
        {
           
            //var records = _context.GetRecords(); // Retrieve your records from the database or any other source.
            //Groups = records.Select(r => r.Group).Distinct().ToList();
            Category = _context.APP_Reports2.Select(x => x.Category).Distinct().ToList();
             
        }

    }
}


