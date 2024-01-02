using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.DirectoryServices.Protocols;
using System.Net;

namespace WebApplication2.Pages
{
    //public class ActiveDirectoryLoginModel : PageModel
    //{
    //    public void OnGet()
    //    {
    //    }
    //}
    public class ActiveDirectoryLoginModel : PageModel
    {



        //post without active directory

        public RedirectToPageResult OnPost()
        {

            var email = Request.Form["email"];
            
            
            return RedirectToPage("/Account/Login", new { area = "Identity", userEmail = email});
            //return RedirectToPage("/Account/Login", new { area = "Identity",email= "keyron.gurvir@free2ducks.com", password= "Fina12@" });
        }


        //post with active directory
        //public IActionResult OnPostLogin()
        //{
        //    try
        //    {
        //        var email = Request.Form["email"];
        //        var password = Request.Form["password"];
        //        using (var ldapConnection = new LdapConnection(new LdapDirectoryIdentifier("ldap://your_domain_controller")))
        //        {
        //            ldapConnection.SessionOptions.ProtocolVersion = 3;

        //            // Bind to the LDAP server using the user's credentials
        //            ldapConnection.Bind(new NetworkCredential(email, password, "your_domain"));

        //            // Authentication successful, proceed with further actions
        //            return RedirectToPage("/Account/Login", new { area = "Identity", userEmail = email });
        //        }
        //    }
        //    catch (LdapException)
        //    {
        //        // Authentication failed, handle accordingly
        //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //        return Page();
        //    }
        //}
    }
}
