using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.GenerateReport
{
    public class MBR300Model : PageModel
    {
        private readonly ApplicationDbContext _context;
        public MBR300Model(ApplicationDbContext context)
        {

            _context = context;
        }



        public List<aMBR300> MBR300 { get; set; }
        public void OnGet()
        {
            MBR300 = _context.MBR300.ToList();
        }

        public void OnPost()
        {
            var editedValue = Request.Form["editedValue"];

            var id = Request.Form["Id"];
            var settingToUpdate = _context.MBR300.Find(int.Parse(id));

            if (settingToUpdate != null)
            {
                settingToUpdate.Amount = decimal.Parse(editedValue);
                _context.SaveChanges();
            }

            MBR300 = _context.MBR300.ToList();

        }
    }
}

