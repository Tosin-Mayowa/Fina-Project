using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.Uploads.ViewState
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<StatesBond> StatesList { get; set; }
        public int Num { get; set; }
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
           
        }
        public void OnGet()
        {
            StatesList = _context.StateUploads.ToList();
            Num = StatesList.Count();
        }
      
        public void OnPost()
        {
            var loanD = Request.Form["loanD"];
            var rate = float.Parse(Request.Form["rate"]);
            DateTime loanDates;
            var isValidDate = DateTime.TryParse(loanD, out loanDates);

            var id = Request.Form["id"];
            var stateList = _context.StateUploads.Find(int.Parse(id));

            if (stateList != null)
            {
                stateList.Date = loanDates;
                stateList.Rate = rate;
               
                _context.SaveChanges();
            }

            StatesList = _context.StateUploads.ToList();

        }
    }
}
