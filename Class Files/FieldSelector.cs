namespace WebApplication2.Class_Files
{

    public class FieldSelector
    {

        public string excelFile { get; set; }
        public string xmlReportStartDate { get; set; }

        public string xmlReportEndDate { get; set; }
        public string Frequency { get; set; }

        public FieldSelector(string category, string excelFilePath, string pStartDate, string pEndDate, string pDate)
        {
            SetData(category, excelFilePath, pStartDate, pEndDate, pDate);
        }

        public void SetData(string category, string excelFilePath, string pStartDate, string pEndDate, string pDate)
        {


            if (category == "DBR")
            {
                excelFile = excelFilePath + "FinaDaily.xlsx";
                xmlReportStartDate = pDate;
                xmlReportEndDate = pDate;
                Frequency = "Daily";
            }
            else if (category == "MBR")
            {
                excelFile = excelFilePath + "FinaMonthly.xlsx";
                xmlReportStartDate = pStartDate;
                xmlReportEndDate = pEndDate;
                Frequency = "Monthly";
            }
            else if (category == "QBR")
            {
                excelFile = excelFilePath + "FinaQuarterly.xlsx";
                xmlReportStartDate = pStartDate;
                xmlReportEndDate = pEndDate;
                Frequency = "Quarterly";
            }
            else if (category == "SBR")
            {
                excelFile = excelFilePath + "FinaSemiAnnual.xlsx";
                xmlReportStartDate = pStartDate;
                xmlReportEndDate = pEndDate;
                Frequency = "Quarterly";
            }

            else if (category == "MTR")
            {
                excelFile = excelFilePath + "MonthlyTrade.xlsx";
                xmlReportStartDate = pStartDate;
                xmlReportEndDate = pEndDate;
                Frequency = "Monthly";
            }

            else if (category == "DTR")
            {
                excelFile = excelFilePath + "DailyTradeTrade.xlsx";
                xmlReportStartDate = pStartDate;
                xmlReportEndDate = pEndDate;
                Frequency = "Daily";
            }
            else
            {
                excelFile = excelFilePath + "CRMS.xlsx";
                xmlReportStartDate = pStartDate;
                xmlReportEndDate = pEndDate;
                Frequency = "Daily";
            }




        }
    }

}
