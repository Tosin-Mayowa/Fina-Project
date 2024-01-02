using System.Data;

namespace WebApplication2.Repository
{
    public interface InterfaceDaily
    {
        string DocumentUpload(IFormFile formFile);

        DataTable LoanUploadTable(string path);

        void ImportLoan(DataTable loanupload);
    }
}
