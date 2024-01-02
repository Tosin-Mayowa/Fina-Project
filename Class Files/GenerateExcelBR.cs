using OfficeOpenXml;
using WebApplication2.Data;
using WebApplication2.Data.Migrations;
using WebApplication2.Model;

namespace WebApplication2.Class_Files
{
    public class GenerateExcelBR
    {

        private readonly ApplicationDbContext _context;
        private readonly ParameterCalls _parmeterCalls;
        private DateTime processDate = DateTime.Now;


        public string spaces8 = new string(' ', 8);
        public string spaces4 = new string(' ', 4);
        public string spaces6 = new string(' ', 6);
        public string spaces2 = new string(' ', 2);
        public string spaces10 = new string(' ', 10);
        public string Pdate = "";
        public DateTime ProcessDate = new DateTime();
        public string excelLocation = "";
        public string XmlLocation = "";
        public string Frequency = "";
        public string excelFilePath = "";
        public string PMonthlydate = "";
        public DateTime StartDate = new DateTime();
        public DateTime EndDate = new DateTime();
        public string PStartDate = "";
        public string PEndDate, FDate = "";
        public string MonthlyXmlStructure = "";
        public string PreportCategory = " ";
        public string subfolderPath = "";
        public string xmlReportStartDate = "";
        public string xmlReportEndDate = "";
        public string excelFile = "";
        public string bankname = "";
        public string bankcode = "";
        //public string freq = "";

        public string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";
        public List<string> result { get; set; }
        public IEnumerable<APP_Report> APP_Reports { get; set; }
        //public strig excelFilePath2 = "";

        //public string CreateFolderAndTextFile(DateTime date)
        public GenerateExcelBR(ApplicationDbContext context, ParameterCalls parmeterCalls)
        {
            _context = context;
            _parmeterCalls = parmeterCalls;

            XmlLocation = _parmeterCalls.GetGlobalValue("FinconXMLLocation");
            excelFilePath = _parmeterCalls.GetGlobalValue("FinconExcelLocation");
            bankcode = _parmeterCalls.GetGlobalValue("CompanyCode");
            bankname = _parmeterCalls.GetGlobalValue("CompanyName");
            ProcessDate = _parmeterCalls.GetDateValue("ProcessDate");
            PMonthlydate = ProcessDate.ToString("dd-MM-yy");
            Pdate = ProcessDate.ToString("dd/MM/yy"); // just
            StartDate = _parmeterCalls.GetDateValue("StartDate");
            EndDate = _parmeterCalls.GetDateValue("EndDate");
            PStartDate = StartDate.ToString("dd/MM/yyyy");
            PEndDate = EndDate.ToString("dd/MM/yyyy");
            FDate = EndDate.ToString("yyyyMM");

            //Pdate = ProcessDate.ToString("dd-MM-yy");
            MonthlyXmlStructure = XmlfileMonthly(FDate);

        }

        public string XmlfileMonthly(string xx)
        {
            return bankcode+"_" + xx + "_CMB_ORIG_";
        }

        public async Task<string> ExcelBRGroup1X(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                if (worksheet==null)
                {
                    return  " File cannot be found";
                }
                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string linecode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col - 3;
                            //colnum = colx - 1;
                            //if (colx <= 9)
                            if (col <= 9)
                            {
                                colno = ".C" + colx+".";
                            }
                            else
                            {
                                colno = ".C" + colx+ ".";
                            }

                            //linecode = worksheet.Cells[row, col].Value;

                            preturncolumn = report_id + colno + worksheet.Cells[row, 3].Value;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupMSR398(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                if (worksheet == null)
                {
                    return " File cannot be found";
                }
                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string linecode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col - 3;
                            //colnum = colx - 1;
                            //if (colx <= 9)
                            if (colx >= 3)
                            {
                                colx = colx + 1;
                            }
                            
                                colno = ".C" + colx + ".";
                            

                            //linecode = worksheet.Cells[row, col].Value;

                            preturncolumn = report_id + colno + worksheet.Cells[row, 3].Value;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup1(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col + 1;
                            colnum = colx - 1;
                            if (col <= 9)
                            {
                                colno = ".C0" + col;
                            }
                            else
                            {
                                colno = ".C" + col;
                            }

                            preturncolumn = report_id + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup1a(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col + 1;
                            colnum = colx - 1;
                            if (col <= 9)
                            {
                                colno = ".C" + col;
                            }
                            else
                            {
                                colno = ".C" + col;
                            }

                            preturncolumn = report_id + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroupT1(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T1.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col + 1;
                            colnum = colx - 1;
                            if (col <= 9)
                            {
                                colno = ".T1.C0" + col;
                            }
                            else
                            {
                                colno = ".T1.C" + col;
                            }

                            string firstSixCharacters = report_id.Substring(0, Math.Min(report_id.Length, 6));
                            //preturncolumn = report_id + colno;
                            preturncolumn = firstSixCharacters + colno;

                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroupT2(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col + 1;
                            colnum = colx - 1;
                            if (col <= 9)
                            {
                                colno = ".T2.C0" + col;
                            }
                            else
                            {
                                colno = ".T2.C" + col;
                            }

                            string firstSixCharacters = report_id.Substring(0, Math.Min(report_id.Length, 6));
                            //preturncolumn = report_id + colno;
                            preturncolumn = firstSixCharacters + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroupT3(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure =  MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col + 1;
                            colnum = colx - 1;
                            if (col <= 9)
                            {
                                colno = ".T3.C0" + col;
                            }
                            else
                            {
                                colno = ".T3.C" + col;
                            }
                            string firstSixCharacters = report_id.Substring(0, Math.Min(report_id.Length, 6));
                            //preturncolumn = report_id + colno;
                            preturncolumn = firstSixCharacters + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup364(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col + 1;
                            colnum = colx - 1;
                            if (col <= 9)
                            {
                                colno = ".C0" + col;
                            }
                            else
                            {
                                colno = ".C" + col;
                            }

                            preturncolumn = report_id + colno;

                            if (preturncolumn == "MBR364.C10") 
                            { 
                            
                            }
                            else 
                            { 
                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                                //writer.WriteLine(); // Move to the next line for the next row
                            }
                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroupGroup2(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    int Colvalue = 4;

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 3; col < colCount; col++)

                        {

                            rownum = row - 1;
                            rowx = rownum + 2;
                            //colx = col + 1;
                            colnum = col - 2;
                            colno = ".C" + colnum;


                            preturncolumn = report_id + colno + ".";
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + worksheet.Cells[row, 3].Value + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col + 1].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                            Colvalue += 1;
                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }





        public async Task<string> ExcelBRGroup500(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    int Colvalue = 4;

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {

                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col - 1;
                            colnum = col - 2;
                            colno = ".C" + col;


                            preturncolumn = report_id + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row


                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup510(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

           
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
               
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                  

                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                  

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {

                            rownum = row - 1;
                            rowx = rownum + 2;
                            //colx = col + 1;
                            colnum = col - 3;
                            colno = ".C" + colnum;


                            preturncolumn = report_id + colno + "." + worksheet.Cells[row, 3].Value;

                            if (preturncolumn == "MBR510.C1.13" || preturncolumn == "MBR510.C1.20" || preturncolumn == "MBR510.C1.27" || preturncolumn == "MBR510.C1.34" ||
                                preturncolumn == "MBR510.C1.41" || preturncolumn == "MBR510.C1.48" || preturncolumn == "MBR510.C1.56")
                            {

                            }
                            else
                            { 

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + worksheet.Cells[row, 2].Value + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                            }
                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup520(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
           

            
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                   


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {

                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col - 1;
                            colnum = col - 2;
                            colno = ".C" + col;


                            preturncolumn = report_id + colno;
                           

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            


                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            
        }

        public async Task<string> ExcelBRGroupGroup540(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    int Colvalue = 4;

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 3; col <= colCount; col++)

                        {

                            rownum = row - 1;
                            rowx = rownum + 2;
                            //colx = col + 1;
                            colnum = col - 2;
                            colno = ".C" + colnum;


                            preturncolumn = report_id + colno + "." + worksheet.Cells[row, 3].Value;

                            if (preturncolumn== "MBR540.C1.0"|| preturncolumn == "MBR540.C2.0"|| preturncolumn == "MBR540.C3.0"||
                                preturncolumn == "MBR540.C4.0") 
                            { 
                            }
                            else 
                            { 

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn  + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                                //writer.WriteLine(); // Move to the next line for the next row
                            }
                            
                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup570(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string typex = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 3; col < colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum + 2;
                            colx = col - 2;
                            colnum = colx - 1;

                            if (row <= 8)
                            {
                                typex = ".T1.C" + colx;
                            }
                            else
                            {
                                typex = ".T2.C" + colx;
                            }


                            //Pcode = worksheet.Cells[row, 3].Value;
                            preturncolumn = report_id + typex + ".";
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + worksheet.Cells[row, 3].Value + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col + 1].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup580(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Display = "";
                    int rownumx = 1;

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {

                            rownum = row-2;
                            rowx = rownum + 2;
                            //colx = col + 1;
                            colnum = col - 3;
                            colno = ".C" + colnum;


                            preturncolumn = report_id + colno + "." + worksheet.Cells[row, 3].Value;
                            
                            Display = "C"+worksheet.Cells[row, 2].Value;

                            if (Display =="C1") 
                            { 
                              
                            }
                            else 
                            {
                                //rownum = rownum - 1;


                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn  + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row,col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");

                                //writer.WriteLine(); // Move to the next line for the next row
                            }
                            
                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup590(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);


            filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    int Colvalue = 4;

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {

                            rownum = row - 2;
                            rowx = rownum + 2;
                            //colx = col + 1;
                            colnum = col - 3;
                            if (colnum >= 9)
                            {
                                colnum = colnum + 1;
                            }
                            colno = ".C" + colnum;



                            preturncolumn = report_id + colno + "." + worksheet.Cells[row, 3].Value;
                            

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn  + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                            Colvalue += 1;
                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup710(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            

            
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                   

                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 2; col <= colCount; col++)

                        {

                            rownum = row - 2;
                            rowx = rownum + 2;
                            colnum = col - 1;
                            colno = ".C" + colnum;


                            preturncolumn = report_id + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row


                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup670(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            colnum = col;


                            if (colnum >= 9)
                            {
                                colnum = colnum + 1;
                            }

                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col - 1;
                            colno = ".C" + colnum;


                            preturncolumn = report_id + colno;
                           

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                           


                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup730(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    int Colvalue = 4;

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {

                            colnum = col;
                            if (colnum >= 11)
                            {
                                colnum = colnum + 1;
                            }

                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col - 1;
                           
                            colno = ".C" + colnum;


                            preturncolumn = report_id + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row


                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup750(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    int Colvalue = 4;

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {

                            rownum = row - 1;
                            rowx = rownum + 2;
                            //colx = col + 1;
                            colnum = col - 3;
                            if (colnum >= 5)
                            {
                                colnum = colnum + 1;
                            }
                            colno = ".C" + colnum;



                            preturncolumn = report_id + colno + ".";
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + worksheet.Cells[row, 3].Value + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                            Colvalue += 1;
                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup810(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {

                            colnum = col;
                            if (colnum >= 7) 
                            {
                                colnum = colnum + 1;
                            }

                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col - 1;
                            
                            colno = ".C" + colnum;


                            preturncolumn = report_id + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row


                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup830(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Display = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {

                            rownum = row - 2;
                            rowx = rownum + 2;
                            //colx = col + 1;
                            colnum = col - 3;
                            colno = ".T0.C0" + colnum;


                            preturncolumn = report_id + colno + "." + worksheet.Cells[row, 3].Value;

                            Display = "C" + worksheet.Cells[row, 2].Value;

                            if (Display == "C1")
                            {

                            }
                            else
                            {

                                writer.WriteLine(spaces6 + "<ITEM>");
                                writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                                writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                                writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                                writer.WriteLine(spaces6 + "</ITEM>");
                                //writer.WriteLine(); // Move to the next line for the next row
                            }

                        }
                    }
                    writer.WriteLine(spaces6 + "<ITEM>");
                    writer.WriteLine(spaces10 + "<ITEMCODE>MBR830.T0.C04.74690</ITEMCODE>");
                    writer.WriteLine(spaces10 + "<ROW>169</ROW>");
                    writer.WriteLine(spaces10 + "<VALUE>0</VALUE>");
                    writer.WriteLine(spaces6 + "</ITEM>");

                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup850(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    int Colvalue = 4;

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {

                            if (col >= 13)
                            {
                                col = col + 1;
                            }

                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col - 1;
                            colnum = col - 2;
                            colno = ".C" + col;


                            preturncolumn = report_id + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row


                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup850a(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";


                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {

                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col - 1;
                            colnum = col;
                            if (colnum >= 13)
                            {
                                colnum = colnum + 1;
                            }
                            colno = ".C" + colnum;


                            preturncolumn = report_id + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row


                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroup660(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

           
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {

                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col - 1;
                            colnum = col - 2;
                            colno = ".C" + colx;


                            preturncolumn = report_id + colno;
                            

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row


                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupT03(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Pcode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 3; col < colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum + 2;
                            colx = col - 2;
                            colnum = colx - 1;
                            if (colx <= 9)
                            {
                                //colno = ".T2.C0" + col;
                                colno = ".T0.C0" + colx;
                            }
                            else
                            {

                                colno = ".T0.C" + colx;
                            }

                            string firstSixCharacters = report_id.Substring(0, Math.Min(report_id.Length, 6));
                            //preturncolumn = report_id + colno;
                            preturncolumn = firstSixCharacters + colno;

                            //preturncolumn = report_id + colno + ".";
                           

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + worksheet.Cells[row, 3].Value + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col + 1].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }




        public async Task<string> ExcelBRGroup300(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

               
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {

                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

           
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";


                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 3; col < colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum + 2;
                            colx = col - 2;
                            colnum = colx - 1;

                            colno = ".T0.C0" + colx;




                            //Pcode = worksheet.Cells[row, 3].Value;
                            preturncolumn = report_id + colno + ".";
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + worksheet.Cells[row, 3].Value + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rowx + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col + 1].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup610(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

           
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";


                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 2; col < colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum - 2;
                            colx = col - 1;
                            colnum = colx - 1;

                            colno = ".C" + colx;


                            //Pcode = worksheet.Cells[row, 3].Value;
                            preturncolumn = report_id + colno + ".";
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + worksheet.Cells[row, 2].Value + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col + 1].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupT02(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Pcode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 2; col < colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum + 2;
                            colx = col - 2;
                            colnum = colx - 1;
                            if (colx <= 9)
                            {
                                //colno = ".T2.C0" + col;
                                colno = ".T0.C0" + colx;
                            }
                            else
                            {

                                colno = ".T0.C" + colx;
                            }
                            //Pcode = worksheet.Cells[row, 3].Value;

                            string firstSixCharacters = report_id.Substring(0, Math.Min(report_id.Length, 6));
                            //preturncolumn = report_id + colno;
                            preturncolumn = firstSixCharacters + colno;
                            //preturncolumn = report_id + colno + ".";
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + worksheet.Cells[row, 2].Value + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col + 1].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroupT02c(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Pcode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum + 2;
                            colx = col - 3;
                            colnum = colx - 1;
                            if (colx <= 9)
                            {
                                //colno = ".T2.C0" + col;
                                colno = ".T0.C0" + colx;
                            }
                            else
                            {

                                colno = ".T0.C" + colx;
                            }
                            //Pcode = worksheet.Cells[row, 3].Value;

                            //string firstSixCharacters = report_id.Substring(0, Math.Min(report_id.Length, 6));
                            //preturncolumn = report_id + colno;
                            //preturncolumn = firstSixCharacters + colno + "." + worksheet.Cells[row, 3].Value;
                            preturncolumn = report_id + colno + "." +  worksheet.Cells[row, 3].Value;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn  + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup560(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

           
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
               
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                 

                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Pcode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum + 2;
                            colx = col - 3;
                            colnum = colx - 1;
                            if (colx <= 9)
                            {
                                //colno = ".T2.C0" + col;
                                colno = ".T0.C0" + colx;
                            }
                            else
                            {

                                colno = ".T0.C" + colx;
                            }
                          
                            preturncolumn = report_id + colno + "." + worksheet.Cells[row, 3].Value;
                            if(preturncolumn== "MBR560.T0.C01.20400") 
                            { 
                            }
                            else 
                            { 
                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                                //writer.WriteLine(); // Move to the next line for the next row
                            }
                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroup360(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Pcode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum + 2;
                            colx = col - 3;
                            colnum = colx - 1;
                           
                                //colno = ".T2.C0" + col;
                                colno = ".T0.C0" + colx; 



                            preturncolumn = report_id + colno + "." + worksheet.Cells[row, 3].Value;
                            if (preturncolumn=="MBR360.T0.C01.33008"|| preturncolumn == "MBR360.T0.C01.33012"||
                                preturncolumn == "MBR360.T0.C01.33020"|| preturncolumn == "MBR360.T0.C01.33022"
                                || preturncolumn == "DBR360.T0.C01.33008"|| preturncolumn == "DBR360.T0.C01.33012" ||
                                preturncolumn == "DBR360.T0.C01.33020" || preturncolumn == "DBR360.T0.C01.33022")
                            {

                            }
                            else
                            {
                                

                                    writer.WriteLine(spaces6 + "<ITEM>");
                                    writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                                    writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                                    writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                                    writer.WriteLine(spaces6 + "</ITEM>");
                                        //writer.WriteLine(); // Move to the next line for the next row
                            }

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupMBR1000(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                 


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Pcode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum + 2;
                            colx = col - 3;
                            colnum = colx - 1;
                           
                            colno = ".T0.C" + colx;
                          
                            preturncolumn = report_id + colno + "." + worksheet.Cells[row, 3].Value;
                            if(preturncolumn== "MBR1000.T0.C2.30140"|| preturncolumn == "MBR1000.T0.C2.30170"||
                                preturncolumn == "MBR1000.T0.C2.30220"|| preturncolumn == "MBR1000.T0.C2.30280"||
                                preturncolumn == "MBR1000.T0.C2.30350"|| preturncolumn == "MBR1000.T0.C2.30400"||
                                preturncolumn == "MBR1000.T0.C2.30420"|| preturncolumn == "MBR1000.T0.C2.30550"||
                                preturncolumn == "MBR1000.T0.C2.30604") 
                            { 
                            }
                            else 
                            { 

                                writer.WriteLine(spaces6 + "<ITEM>");
                                writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                                writer.WriteLine(spaces10 + "<ROW>" + worksheet.Cells[row, 2].Value + "</ROW>");
                                writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                                writer.WriteLine(spaces6 + "</ITEM>");
                                //writer.WriteLine(); // Move to the next line for the next row
                            }

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupMBR1002(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Pcode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 3; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col - 2;
                            colnum = colx - 1;
                            if (colx <= 9)
                            {
                                //colno = ".T2.C0" + col;
                                colno = ".T0.C0" + colx;
                            }
                            else
                            {

                                colno = ".T0.C" + colx;
                            }

                            preturncolumn = report_id + colno + "." + worksheet.Cells[row, 2].Value;


                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupMBR1006(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Pcode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 4; col <= colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum + 2;
                            colx = col - 3;
                            colnum = colx - 1;
                            if (colx <= 9)
                            {
                                //colno = ".T2.C0" + col;
                                colno = ".T0.C0" + colx;
                            }
                            else
                            {

                                colno = ".T0.C" + colx;
                            }
                           
                            preturncolumn = report_id + colno + "." + worksheet.Cells[row, 3].Value;
                           

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + worksheet.Cells[row, 2].Value + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupT02d(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Pcode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 3; col <= colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum + 2;
                            colx = col - 2;
                            colnum = colx - 1;
                            if (colx <= 9)
                            {
                                //colno = ".T2.C0" + col;
                                colno = ".T0.C0" + colx;
                            }
                            else
                            {

                                colno = ".T0.C" + colx;
                            }
                            //Pcode = worksheet.Cells[row, 3].Value;

                            //string firstSixCharacters = report_id.Substring(0, Math.Min(report_id.Length, 6));
                            //preturncolumn = report_id + colno;
                            //preturncolumn = firstSixCharacters + colno + "." + worksheet.Cells[row, 3].Value;
                            preturncolumn = report_id + colno + "." + worksheet.Cells[row, 2].Value;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupT02b(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    string Pcode = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 3; col <= colCount; col++)

                        {
                            rownum = row - 1;
                            rowx = rownum + 2;
                            colx = col - 2;
                            colnum = colx - 1;
                            if (colx <= 9)
                            {
                                //colno = ".T2.C0" + col;
                                colno = ".T0.C0" + colx;
                            }
                            else
                            {

                                colno = ".T0.C" + colx;
                            }
                            //Pcode = worksheet.Cells[row, 3].Value;

                            string firstSixCharacters = report_id.Substring(0, Math.Min(report_id.Length, 6));
                            //preturncolumn = report_id + colno;
                            preturncolumn = firstSixCharacters + colno + "." + worksheet.Cells[row, 2].Value;
                            //preturncolumn = report_id + colno + ".";
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupT0(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T3.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col + 1;
                            colnum = colx - 1;
                            if (col <= 9)
                            {
                                colno = ".T0.C0" + col;
                            }
                            else
                            {
                                colno = ".T0.C" + col;
                            }

                            //string firstSixCharacters = report_id.Substring(0, Math.Min(report_id.Length, 6));
                            preturncolumn = report_id + colno;
                            //preturncolumn = firstSixCharacters + colno;

                            //preturncolumn = report_id + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroup394(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            if (Frequency == "Daily")
            {

                //filestructure = "/"+ report_id;
                filestructure = "/" + report_id + ".xml";
                //filestructure = report_id + ".xml";
            }
            else
            {
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";
            }

            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col + 1;
                            colnum = colx - 1;

                            colno = ".C" + col;


                            preturncolumn = report_id + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupQBR1(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            //filestructure = MonthlyXmlStructure + "/"+ report_id;
            //filestructure = MonthlyXmlStructure + report_id + ".xml";
            filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
               
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";


                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {

                            rowx = row - 2;
                            colno = ".C" + col;
                                                        
                            preturncolumn = report_id + colno;
                           

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn +"</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rowx +"</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupQBR1360(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            //filestructure = MonthlyXmlStructure + "/"+ report_id;
            //filestructure = MonthlyXmlStructure + report_id + ".xml";
            filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;




                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";


                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {

                            rowx = row - 2;
                            colnum = col;
                            if (colnum >= 7) 
                            {
                                colnum = colnum + 1;
                                
                            }
                            colno = ".C" + colnum;

                            preturncolumn = report_id + colno;


                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rowx + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }

        public async Task<string> ExcelBRGroupQBR2(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);
              
            //filestructure = MonthlyXmlStructure + "/"+ report_id;
            //filestructure = MonthlyXmlStructure + report_id + ".xml";
            filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;




                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";


                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 3; col <= colCount; col++)

                        {
                            colx = col - 2;
                            rowx = row - 2;
                            colno = ".C" + colx;

                            preturncolumn = report_id + colno + ".";


                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + worksheet.Cells[row, 2].Value +"</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rowx + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroupQBR1330(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            //filestructure = MonthlyXmlStructure + "/"+ report_id;
            //filestructure =  MonthlyXmlStructure + report_id + ".xml";
            filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx, coly = 0;
                    string colno = ".T2.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";


                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            colx = col;
                            rowx = row - 2;
                            colno = ".C" + colx;

                            if (colx < 3) { coly = 1; }
                            else { coly = colx - 2; }

                            if (colx == 1) { colno = ".C" + coly; }
                            else if (colx == 2) { colno = ".C" + coly + "a"; }
           
                            else if (colx == 3) { colno = ".C" + coly + "b"; }
                            else { colno = ".C" + coly; }
 

                            preturncolumn = report_id + colno;


                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn +"</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rowx + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroupSBRT1(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

                        
                //filestructure = MonthlyXmlStructure + "/"+ report_id;
                //filestructure = MonthlyXmlStructure + report_id + ".xml";
                filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";



            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                  

                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T1.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";
                    int row = 0;
                    

                    //for (int row = 2; row <= rowCount; row++)
                    row = 2;

                    {
                        for (int col = 2; col <= colCount; col++)

                        {
                            colx = col - 1;



                            colno = ".T1.A.C" + colx+".1";

                            preturncolumn = "SBR1910" + colno;
                            

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>0</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }

                    row = 4;

                    {
                        for (int col = 2; col <= colCount; col++)

                        {
                            colx = col-1;



                            colno = ".T1.C" + colx + ".04";

                            preturncolumn = "SBR1910" + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>0</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }

                    row = 6;

                    {
                        for (int col = 2; col <= colCount; col++)

                        {
                            colx = col-1;



                            colno = ".T1.C.C" + colx + ".03";

                            preturncolumn = "SBR1910" + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>0</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }


                    row = 8;

                    {
                        for (int col = 2; col <= colCount; col++)

                        {
                            colx = col-1;



                            colno = ".T1.D.C" + colx + ".03";

                            preturncolumn = "SBR1910" + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>0</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }

                    row = 10;

                    {
                        for (int col = 2; col <= colCount; col++)

                        {
                            colx = col-1;



                            colno = ".T1.E.C" + colx + ".01";

                            preturncolumn = "SBR1910" + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>0</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }

                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroupSBRT2(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);


            //filestructure = MonthlyXmlStructure + "/"+ report_id;
            // filestructure = MonthlyXmlStructure + report_id + ".xml";
            filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";


            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
               
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T1.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col + 1;
                            colnum = colx - 1;
                            if (colx <= 9)
                            {
                                colno = ".T2.C0" + col;
                            }
                            else
                            {
                                colno = ".T2.C" + colx;
                            }

                            preturncolumn = "SBR1910" + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroupSBR19103(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);


            //filestructure = MonthlyXmlStructure + "/"+ report_id;
            //filestructure = MonthlyXmlStructure + report_id + ".xml";
            filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";


            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                  

                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T1.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col + 1;
                            colnum = colx - 1;
                            if (col <= 9)
                            {
                                colno = ".T3.C0" + col;
                            }
                            else
                            {
                                colno = ".T3.C" + col;
                            }

                            preturncolumn = "SBR1910" + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelSBRGroup1(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);

            //filestructure = MonthlyXmlStructure + "/"+ report_id;
            // filestructure = MonthlyXmlStructure + report_id + ".xml";
            filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";

            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col;
                            colnum = colx - 1;
                            
                            
                                colno = ".C" + colx;
                            

                            preturncolumn = report_id + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }


        public async Task<string> ExcelBRGroupSBR19104(string subfolderPath, string excelFilePath, string Frequency, string startDate, string endDate, string reportcode)
        {
            string filestructure = "";
            string report_id = reportcode;
            string Report_Desc = _parmeterCalls.reportcodr(report_id);


            //filestructure = MonthlyXmlStructure + "/"+ report_id;
            //filestructure = MonthlyXmlStructure + report_id + ".xml";
            filestructure = "/" + MonthlyXmlStructure + report_id + ".xml";


            //string Pdate =date.ToString("dd-MM-yy");

            // Specify the path to your Excel file
            // string excelFilePath = "C:/Users/ADMIN/Desktop/Fina Project/Fincon/FinConExcel/FinAMonthly.xlsx";

            // Specify the path to the text file where you want to write the rows
            //string textFilePath = "C:/Users/ADMIN/Desktop/DBR3001.xml";
            string textFilePath = subfolderPath + filestructure;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Assume the Excel sheet is the first one (index 0
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                ExcelWorksheet worksheet = package.Workbook.Worksheets[report_id];

                using (System.IO.StreamWriter writer = new StreamWriter(textFilePath))
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    //string xmlContent1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";


                    writer.WriteLine(xmlContent1);

                    writer.WriteLine(spaces4 + "<RETURN>");
                    writer.WriteLine(spaces6 + "<HEADER>");
                    writer.WriteLine(spaces8 + "<BANKCODE>" + bankcode + "</BANKCODE>");
                    writer.WriteLine(spaces8 + "<BANKNAME>" + bankname + "</BANKNAME>");
                    writer.WriteLine(spaces8 + "<RETURNCODE>" + report_id + "</RETURNCODE>");
                    writer.WriteLine(spaces8 + "<RETURNNAME>" + Report_Desc + "</RETURNNAME>");
                    writer.WriteLine(spaces8 + "<PERIODFROM>" + startDate + "</PERIODFROM>");
                    writer.WriteLine(spaces8 + "<PERIODEND>" + endDate + "</PERIODEND>");
                    writer.WriteLine(spaces8 + "<VER>ORIG</VER>");
                    writer.WriteLine(spaces8 + "<SIGNED/>");
                    writer.WriteLine(spaces8 + "<LNG>en_US</LNG>");
                    writer.WriteLine(spaces6 + "</HEADER>");
                    writer.WriteLine(spaces6 + "<BODY>");


                    int rownum = 0;
                    int colx = 0;
                    string colno = ".T1.C0";
                    int rowx = 0;
                    int colnum = 0;
                    string preturncolumn = "";

                    for (int row = 2; row <= rowCount; row++)

                    {
                        for (int col = 1; col <= colCount; col++)

                        {
                            rownum = row - 2;
                            rowx = rownum + 2;
                            colx = col + 1;
                            colnum = colx - 1;
                            if (col <= 9)
                            {
                                colno = ".T4.C0" + col;
                            }
                            else
                            {
                                colno = ".T4.C" + col;
                            }

                            preturncolumn = "SBR1910" + colno;
                            // {
                            // Write each column value to the text file
                            // writer.Write(worksheet.Cells[row, col].Value?.ToString() ?? "");
                            // writer.Write("\t"); // Assuming you want to separate columns with a tab
                            //}
                            //var cellValue = worksheet.Cells[row, col].Value;

                            writer.WriteLine(spaces6 + "<ITEM>");
                            writer.WriteLine(spaces10 + "<ITEMCODE>" + preturncolumn + "</ITEMCODE>");
                            writer.WriteLine(spaces10 + "<ROW>" + rownum + "</ROW>");
                            writer.WriteLine(spaces10 + "<VALUE>" + worksheet.Cells[row, col].Value + "</VALUE>");
                            writer.WriteLine(spaces6 + "</ITEM>");
                            //writer.WriteLine(); // Move to the next line for the next row

                        }
                    }
                    writer.WriteLine(spaces6 + "</BODY >");
                    writer.WriteLine(spaces4 + "</RETURN >");
                    return report_id + "_" + Report_Desc + " successfully created.";
                }

            }


            // Read an Excel file and write to a text file (Method 2)
        }






































    }

}
