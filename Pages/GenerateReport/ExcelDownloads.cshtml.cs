using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.ComponentModel;
using System.Data;
using WebApplication2.Data;
using WebApplication2.Data.Migrations;
using WebApplication2.Model;

namespace WebApplication2.Pages.GenerateReport
{
    public class ExcelDownloadsModel : PageModel
    {
        private readonly ApplicationDbContext _DbContext;

        [BindProperty]
        public List<int> SelectedReports { get; set; }

        public List<string> Departments { get; set; }
        public string Categories { get; set; }
        public string Dept { get; set; }
        public IEnumerable<APP_Report> APP_Reportsx { get; set; }
        public IEnumerable<APP_Report> APP_Reportsx2 { get; set; }

        public ExcelDownloadsModel(ApplicationDbContext db)
        {
            _DbContext = db;

        }
        public void OnGet(string category)
        {
            Categories = category;
            // APP_Reportsx = _DbContext.APP_Reports.Where(r => r.Category == category).ToList();
            //APP_Reports = _niyi.APP_Report.Where(r => r.Category == category).ToList();
            APP_Reportsx = _DbContext.APP_Reports2.Where(r => r.Category == category);
            APP_Reportsx2 = APP_Reportsx.Where(r => r.Datalink1 != null);
            Departments = APP_Reportsx2.Select(x => x.Department.ToUpper()).ToList();

        }



        public IActionResult OnPost(string category)

        {

            APP_Reportsx = _DbContext.APP_Reports2.Where(r => r.Category == category);
            APP_Reportsx2 = APP_Reportsx.Where(r => r.Datalink1 != "xx");
            var is_available = category;
            Console.WriteLine(is_available);


            if (SelectedReports != null && SelectedReports.Count > 0)
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())

                {
                    foreach (var reportId in SelectedReports)
                    {
                        var report = APP_Reportsx2.FirstOrDefault(r => r.NO == reportId);
                        if (report != null)
                        {
                            var tableData = GetTableData(report.Datalink1); // Replace with your data retrieval logic

                            var worksheet = package.Workbook.Worksheets.Add(report.Report_Description);
                            // Populate the worksheet with the data from tableData
                            for (int i = 0; i < tableData.Columns.Count; i++)
                            {
                                worksheet.Cells[1, i + 1].Value = tableData.Columns[i].ColumnName;
                            }

                            // Populate the data rows
                            for (int row = 0; row < tableData.Rows.Count; row++)
                            {
                                for (int col = 0; col < tableData.Columns.Count; col++)
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = tableData.Rows[row][col];
                                }
                            }

                        }
                    }

                    var excelBytes = package.GetAsByteArray();
                    return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SelectedReports.xlsx");
                }
            }

            return Page();
        }


        private DataTable GetTableData(string tableName)
        {
            using (var connection = new SqlConnection(_DbContext.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand($"SELECT * FROM {tableName}", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }

        }








    }
}