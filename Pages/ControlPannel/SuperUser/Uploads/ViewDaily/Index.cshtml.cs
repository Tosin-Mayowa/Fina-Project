using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.Uploads.ViewDaily
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<DailyDate> DailyList { get; set; }
        public int Num { get; set; }
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            DailyList = _context.U_Daily.ToList();
            Num = DailyList.Count();
        }

        public void OnPost()
        {
            var loanD = Request.Form["loanD"];
            var rate = float.Parse(Request.Form["rate"]);
            DateTime loanDates;
         var isValidDate= DateTime.TryParse(loanD, out loanDates);
            
            var id = Request.Form["id"];
            var dailyList = _context.U_Daily.Find(int.Parse(id));

            if (dailyList != null)
            {
                
                dailyList.Date = loanDates;
                dailyList.Rate = rate;
               
                
                _context.SaveChanges();
            }

            DailyList = _context.U_Daily.ToList();

        }
    }
}
