using System.Data;

namespace WebApplication2.Repository
{
    public interface InterfaceCashflows
    {
        string DocumentUpload(IFormFile formFile);

        DataTable LoanUploadTable(string path);

        void ImportLoan(DataTable loanupload);
    }
}
