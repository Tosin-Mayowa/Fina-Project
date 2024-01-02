using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace WebApplication2.Repository
{
    public class UploadDetails : InterfaceUpload
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment _webHostEnvironment;

        public UploadDetails(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {

            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public DataTable LoanUploadTable(string path)
        {
            var constr = _configuration.GetConnectionString("excelconnection");
            DataTable dataTable = new DataTable();
            constr = string.Format(constr, path);
            using (OleDbConnection excelconn = new OleDbConnection(constr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter adapterexcel = new OleDbDataAdapter())
                    {
                        excelconn.Open();
                        cmd.Connection = excelconn;
                        DataTable excelschema;
                        excelschema = excelconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        var sheetname = excelschema.Rows[0]["Table_Name"].ToString();
                        excelconn.Close();
                        cmd.CommandText = "Select * from [" + sheetname + "]";
                        adapterexcel.SelectCommand = cmd;
                        adapterexcel.Fill(dataTable);
                        excelconn.Close();
                    }
                }
            }
            return dataTable;
        }

        public string DocumentUpload(IFormFile formFile)
        {
            string uploadPath = _webHostEnvironment.WebRootPath;
            string dest_path = Path.Combine(uploadPath, "upload_doc");
            if (!Directory.Exists(dest_path))
            {
                Directory.CreateDirectory(dest_path);
            }
            string sourceFile = Path.GetFileName(formFile.FileName);
            string path = Path.Combine(dest_path, sourceFile);
            using (FileStream filesStream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(filesStream);
            }
            return path;
        }

        public void ImportLoan(DataTable loanupload)
        {
            var sqlconn = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection scon = new SqlConnection(sqlconn))

            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(scon))

                {

                    sqlBulkCopy.DestinationTableName = "LoanUploads1";
                    sqlBulkCopy.ColumnMappings.Add("AccountName", "AccountName");
                    sqlBulkCopy.ColumnMappings.Add("SectorCode", "SectorCode");
                    sqlBulkCopy.ColumnMappings.Add("IndustryCode", "IndustryCode");
                    sqlBulkCopy.ColumnMappings.Add("AmortizationAmt", "AmortizationAmt");
                    sqlBulkCopy.ColumnMappings.Add("TotalImpairment", "TotalImpairment");
                    sqlBulkCopy.ColumnMappings.Add("Staging", "Staging");
                    sqlBulkCopy.ColumnMappings.Add("Classification", "Classification");
                    sqlBulkCopy.ColumnMappings.Add("Performance", "Performance");
                    sqlBulkCopy.ColumnMappings.Add("Performance1", "Performance1");
                    sqlBulkCopy.ColumnMappings.Add("Performance2", "Performance2");
                    sqlBulkCopy.ColumnMappings.Add("Performance3", "Performance3");
                    sqlBulkCopy.ColumnMappings.Add("Performance4", "Performance4");

                    sqlBulkCopy.BulkCopyTimeout = 36000;
                    scon.Open();
                    sqlBulkCopy.WriteToServer(loanupload);
                    scon.Close();

                }

            }
        }

    }

   
   

}
