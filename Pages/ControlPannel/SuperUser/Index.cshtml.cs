using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Drawing2D;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        
       
        private readonly ApplicationDbContext _db;
  
        
        public IEnumerable<DateSettings> DateSettings { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            DateSettings = _db.DateSettings;
           
        }

        
    }
}
