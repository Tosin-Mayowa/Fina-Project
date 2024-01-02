using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.GenerateReport
{
    public class DirectUpdateModel : PageModel
    {
        private readonly ApplicationDbContext _DbContext;

        [BindProperty]
        public List<int> SelectedReports { get; set; }

        public List<string> Departments { get; set; }
        public string Categories { get; set; }
        public string Dept { get; set; }
        public IEnumerable<APP_Report> APP_Reportsx { get; set; }
        public IEnumerable<APP_Report> APP_Reportsx2 { get; set; }

        public DirectUpdateModel(ApplicationDbContext db)
        {
            _DbContext = db;

        }
        public void OnGet(string category)
        {
            Categories = category;
            // APP_Reportsx = _DbContext.APP_Reports.Where(r => r.Category == category).ToList();
            //APP_Reports = _niyi.APP_Report.Where(r => r.Category == category).ToList();
            APP_Reportsx = _DbContext.APP_Reports2.Where(r => r.Category == category);
            APP_Reportsx2 = APP_Reportsx.Where(r => r.Datalink1 != null);
            Departments = APP_Reportsx2.Select(x => x.Department.ToUpper()).ToList();

        }



        

            
    }
}
