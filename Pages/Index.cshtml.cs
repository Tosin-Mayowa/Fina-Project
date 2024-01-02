using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;
using WebApplication2.Model;

namespace WebApplication2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IndexModel(ILogger<IndexModel> logger, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }
       
        public RedirectToPageResult OnGet()
        {

            //if (_signInManager.IsSignedIn(User))
            //{
            //    return RedirectToPage("Index");
            //    return LocalRedirect("~/Page"); use this if the above did not work

            //}
            return RedirectToPage("/ActiveDirectoryLogin");
            //return RedirectToPage("/Account/Login", new { area = "Identity",email= "keyron.gurvir@free2ducks.com", password= "Fina12@" });
            

        }
    }
}