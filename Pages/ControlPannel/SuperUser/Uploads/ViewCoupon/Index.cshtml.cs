using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.Uploads.ViewCoupon
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<Coupons> CouponsList { get; set; }
        public int Num { get; set; }
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            CouponsList = _context.CouponUploads.ToList();
            Num = CouponsList.Count();
        }

        public void OnPost()
        {
            var loanD = Request.Form["loanD"];
            var rate = float.Parse(Request.Form["rate"]);
            DateTime loanDates;
            var isValidDate = DateTime.TryParse(loanD, out loanDates);

            var id = Request.Form["id"];

        
            var couponList = _context.CouponUploads.Find(int.Parse(id));

            if (couponList != null)
            {
                couponList.Date = loanDates;
                couponList.Rate = rate;
               
                _context.SaveChanges();
            }

            CouponsList = _context.CouponUploads.ToList();

        }
    }
}
