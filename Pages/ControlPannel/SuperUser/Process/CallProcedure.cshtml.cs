using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication2.Data;

namespace WebApplication2.Pages.ControlPannel.SuperUser.Process
{
    public class CallProcedureModel : PageModel
    {
        private IConfiguration configuration;
        private IWebHostEnvironment webHostEnvironment;

        private readonly ApplicationDbContext _context;


        public CallProcedureModel(IConfiguration configuration, ApplicationDbContext context)
        {

            this.configuration = configuration;
            _context = context;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Request.Query.TryGetValue("procedureName", out var procedureName))
            {
                var sqlconn = configuration.GetConnectionString("sqlConnection");

                //string connectionString = "your_connection_string_here";

                using (SqlConnection connection = new SqlConnection(sqlconn))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        try
                        {
                            command.ExecuteNonQuery();
                            ViewData["Message"] = $"{procedureName} executed successfully.";
                        }
                        catch (Exception ex)

                        {
                            ViewData["ErrorMessage"] = $"Error executing {procedureName}: {ex.Message}";
                        }
                    }
                }
            }

            return Page();
        }
    }
}
