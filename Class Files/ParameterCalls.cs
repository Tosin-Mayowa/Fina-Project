using WebApplication2.Data;

namespace WebApplication2.Class_Files
{
    public class ParameterCalls
    {

        private readonly ApplicationDbContext _context;





        public ParameterCalls(ApplicationDbContext context)
        {
            _context = context;

        }

        public DateTime GetDateValue(string datePurpose)
        {
            return _context.DateSettings
                .Where(d => d.DatePurpose == datePurpose) // Adjust your query conditions
                .Select(d => d.DateActual)
                .FirstOrDefault();
        }


        public DateTime GetStartDate()
        {
            return _context.DateSettings
                .Where(d => d.DatePurpose == "StartMonthDate") // Adjust your query conditions
                .Select(d => d.DateActual)
                .FirstOrDefault();
        }

        public DateTime GetEndMonthDate()
        {
            return _context.DateSettings
                .Where(d => d.DatePurpose == "EndMonthDate") // Adjust your query conditions
                .Select(d => d.DateActual)
                .FirstOrDefault();
        }

        public string GetGlobalValue(string name)
        {
            return  _context.APP_GlobalSettings
                .Where(r => r.Name == name) // Adjust your query conditions
                .Select(r => r.Value)
                .FirstOrDefault();
        }
        public string reportcodr(string reportId)
        {
            return _context.APP_Reports2
                .Where(d => d.Report_ID == reportId) // Adjust your query conditions
                .Select(d => d.Report_Description)
                .FirstOrDefault();
        }

        public string reporttable(string reportId)
        {
            return _context.APP_Reports2
                .Where(d => d.Report_ID == reportId) // Adjust your query conditions
                .Select(d => d.Datalink1)
                .FirstOrDefault();
        }

        public string GetReportFrequency(string reportId)
        {
            return _context.APP_Reports2
                .Where(d => d.Report_ID == reportId) // Adjust your query conditions
                .Select(d => d.Frequency)
                .FirstOrDefault();
        }

        public string GetCategoryFrequency(string reportId)
        {
            return _context.APP_Reports2
                .Where(d => d.Report_ID == reportId) // Adjust your query conditions
                .Select(d => d.Frequency)
                .FirstOrDefault();


        }





        public string CreateFolderAndTextFile(string Pdate, string reportCategory, string XmlLocation)
        {
            //string folderName = reportCategory + date.ToString("yyyy-MM-dd");
            string folderName = reportCategory + "_" + Pdate;
            //string folderName = "DBR_xxx";

            string folderPath = Path.Combine(XmlLocation, folderName); // Replace "YourFolderPath" with the desired folder path
            //string filePath = Path.Combine(folderPath, "TEST405.txt");
            string subfolderPath = Path.Combine(folderPath, folderName);
            //string filePath = Path.Combine(subfolderPath, "TEST405.txt");




            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!Directory.Exists(subfolderPath))
            {
                Directory.CreateDirectory(subfolderPath);
            }

            return subfolderPath;

        }






    }
}
