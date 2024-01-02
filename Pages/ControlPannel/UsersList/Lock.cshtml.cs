using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication2.Pages.ControlPannel.UsersList
{
    public class LockModel : PageModel
    {
        public string Email { get; set; }
        public void OnGet(string userEmail)
        {
            Email = userEmail;
        }
    }
}
