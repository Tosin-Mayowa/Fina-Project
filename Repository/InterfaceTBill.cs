using System.Data;

namespace WebApplication2.Repository
{
    public interface InterfaceTBill
    {
        string DocumentUpload(IFormFile formFile);

        DataTable LoanUploadTable(string path);

        void ImportLoan(DataTable loanupload);
    }
}
