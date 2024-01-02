using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace WebApplication2.Pages.ControlPannel.UsersList
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public ICollection<ApplicationUser> UsersList { get; set; }
        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager=userManager;
            UsersList = _userManager.Users.ToList();
        }
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostLockoutAsync(string email)
        {


            try
            {
                string userEmail = email;

              
                var user = _userManager.Users.FirstOrDefault(u => u.Email == userEmail);
                
                if (user != null)
                {
                    Console.WriteLine("found");
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
                    // Redirect to a success page or refresh the current page

                    return RedirectToPage("/ControlPannel/UsersList/Lock", new { userEmail = email });
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log, display an error message, or redirect to an error page).
                // Example: Log the exception to the console.
                Console.WriteLine($"An error occurred while finding the user: {ex.Message}");

                // You can choose to handle the error in a way that best suits your application's needs.
                 // Redirect to an error page, if available.
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUnlockAsync(string email)
        {
            string userEmail = email;
            var user = _userManager.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user != null)
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
                // Redirect to a success page or refresh the current page
                return RedirectToPage("/ControlPannel/UsersList/Unlock");
            }
            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(string email)
        {
            string userEmail = email;
            var user = _userManager.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                // Redirect to a success page or refresh the current page
                return RedirectToPage("/ControlPannel/UsersList/Index");
            }
            return Page();
        }





    }
}

