using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.Uploads.ViewCashFlow
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<CashFlowUpload> CashFlowList { get; set; }
        public int Num { get; set; }
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            CashFlowList = _context.CashFlows.ToList();
            Num = CashFlowList.Count;
        }

        public void OnPost()
        {
            var loanD = Request.Form["loanD"];
            var rate = float.Parse(Request.Form["rate"]);
            DateTime loanDates;
            var isValidDate = DateTime.TryParse(loanD, out loanDates);

            var id = Request.Form["id"];
            var cashflowList = _context.CashFlows.Find(int.Parse(id));

            if (cashflowList != null)
            {

                cashflowList.Date = loanDates;
                cashflowList.Rate = rate;
                _context.SaveChanges();
            }

           CashFlowList = _context.CashFlows.ToList();

        }
    }
}
