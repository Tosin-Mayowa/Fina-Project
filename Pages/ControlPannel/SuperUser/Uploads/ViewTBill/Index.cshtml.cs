using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.Uploads.ViewTBill
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<TBillFmdq> TBill { get; set; }
        public int Num { get; set; }
        public IndexModel(ApplicationDbContext context)
        {
            _context=context;
        }
        public void OnGet()
        {
            TBill  = _context.TBills.ToList();
            Num = TBill.Count();
        }

        public void OnPost()
        {
            var loanD = Request.Form["loanD"];
            var rate = float.Parse(Request.Form["rate"]);
            DateTime loanDates;
            var isValidDate = DateTime.TryParse(loanD, out loanDates);

            var id = Request.Form["id"];
            var tbillList = _context.TBills.Find(int.Parse(id));

            if (tbillList != null)
            {
                tbillList.Date = loanDates;
                tbillList.Rate = rate;
                
                _context.SaveChanges();
            }

            TBill = _context.TBills.ToList();

        }
    }
}
