using System.Data;

namespace WebApplication2.Repository
{
    public interface InterfaceUpload
    {
        string DocumentUpload(IFormFile formFile);

        DataTable LoanUploadTable(string path);

        void ImportLoan(DataTable loanupload);
    }
}
