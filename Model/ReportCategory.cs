using System.Net.NetworkInformation;

namespace WebApplication2.Model
{
    public class ReportCategory
    {

        public int Id { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string Department { get; set; }
        public string Frequency { get; set; }

    }
}
