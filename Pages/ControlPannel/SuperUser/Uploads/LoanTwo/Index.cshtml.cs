using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser.Uploads.LoanTwo
{
    public class IndexModel : PageModel
    {
        public bool Is_Done = true;
        public int Num { get; set; }
        private readonly Repository.InterfaceLoanTwo _loanupload;
        private IConfiguration _configuration;
        private IWebHostEnvironment _webHostEnvironment;
        public ICollection<LoanUploads2> LoanUploadFields { get; set; }
        private readonly ApplicationDbContext _db;
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public IndexModel(WebApplication2.Repository.InterfaceLoanTwo loanupload, IConfiguration configuration, Data.ApplicationDbContext context, ApplicationDbContext db)
        {
            _loanupload = loanupload;
            _configuration = configuration;
            _context = context;
            _db = db;
        }


        public async void OnGetAsync()
        {

        }

        public async Task<IActionResult> OnPostAsync(IFormFile formFile)
        {


            var num = await _context.LoanUpload2.ToListAsync();
            Num = num.ToList().Count();
            Is_Done = false;
            if (Num != 0)
            {
                await _db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE LoanUpload2");

                ViewData["DeleteDone"] = "Table successfully deleted";

                string path = _loanupload.DocumentUpload(formFile);
                DataTable dt = _loanupload.LoanUploadTable(path);
                _loanupload.ImportLoan(dt);

                //return Page();
                var sqlconn = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection scon = new SqlConnection(sqlconn))
                {
                    await scon.OpenAsync();
                    using (var commd = new SqlCommand("Select count(*) from LoanUpload2", scon))
                    {
                        var result = await commd.ExecuteScalarAsync();
                        ViewData["RecordCount"] = result.ToString();


                    }

                }
                Is_Done = true;
            }
            else
            {

                string path = _loanupload.DocumentUpload(formFile);
                DataTable dt = _loanupload.LoanUploadTable(path);
                _loanupload.ImportLoan(dt);

                //return Page();
                var sqlconn = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection scon = new SqlConnection(sqlconn))
                {
                    await scon.OpenAsync();
                    using (var commd = new SqlCommand("Select count(*) from LoanUpload2", scon))
                    {
                        var result = await commd.ExecuteScalarAsync();
                        ViewData["RecordCount"] = result.ToString();


                    }

                }
                Is_Done = true;
            }



            return Page();
        }

    }
}
