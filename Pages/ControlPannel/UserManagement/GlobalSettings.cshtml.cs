using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.UserManagement
{
    public class GlobalSettingsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public GlobalSettingsModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }



        public List<APP_GlobalSetting> GlobalSettings { get; set; }
        public void OnGet()
        {
            GlobalSettings = _context.APP_GlobalSettings.ToList();
        }

        public void OnPost()
        {
            var editedValue = Request.Form["editedValue"];
            
            var id = Request.Form["id"];
            var settingToUpdate = _context.APP_GlobalSettings.Find(int.Parse(id));

            if (settingToUpdate != null)
            {
                settingToUpdate.Value = editedValue;
                _context.SaveChanges();
            }

            GlobalSettings = _context.APP_GlobalSettings.ToList();

        }
    }
}
