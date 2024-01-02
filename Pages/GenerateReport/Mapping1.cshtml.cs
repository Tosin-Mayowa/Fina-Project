using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using System.Linq.Dynamic.Core;
using WebApplication2.Class_Files;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.GenerateReport
{
    public class Mapping1Model : PageModel
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly GenerateExcelBR _generateExcelBR;
        private readonly ParameterCalls _parameterCalls;
        //private readonly Downloadable _downloadable;
        public string Categories = "";

        [BindProperty(SupportsGet = true)]
        public string Category { get; set; }
        [BindProperty]
        public List<bool> SelectedReports { get; set; }
        //public List<bool> SelectedReports { get; set; }
        public IEnumerable<APP_Report> APP_Reportsx { get; set; }
        public List<APP_Report> Reports { get; set; }
        public ICollection<string> ResultMessages { get; set; }
        //public List<string> ResultMessages { get; set; }
        public int ReportNum { get; set; }
        public List<APP_Report> Reports2 { get; set; }
        public List<APP_Report> filteredReports { get; set; }
        public string categoryToFilter = "MBR"; /// Look for how to automate
        public string categoryValue { get; set; }
        public DateTime ProcessDate = DateTime.Now;
        public string pdate = "";
        public string excelLocation = "";
        public string XmlLocation = "";
        public string Frequency { get; set; }
        public string excelFilePath = "";
        public string PMonthlydate = "";
        public DateTime StartDate = new DateTime();
        public DateTime EndDate = new DateTime();
        public string PStartDate = "";
        public string PEndDate = "";
        public string MonthlyXmlStructure = "";
        public string subfolderPath = "";
        public string xmlReportStartDate { get; set; }
        public string xmlReportEndDate { get; set; }
        public string freq = "";
        public string excelFile { get; set; }
        public string pStartDate, pEndDate = "";
        public List<string> Departments { get; set; }
        public string Dept { get; set; }
        public int SelectedCount = 0;

        public Mapping1Model(ApplicationDbContext db, ParameterCalls parameterCalls, GenerateExcelBR generateExcelBR)
        {
            _DbContext = db;
            _generateExcelBR = generateExcelBR;
            _parameterCalls = parameterCalls;
            Reports = _DbContext.APP_Reports2.ToList();
            SelectedReports = Enumerable.Repeat(false, Reports.Count).ToList();
            XmlLocation = _parameterCalls.GetGlobalValue("FinconXmlPath");
            XmlLocation = _generateExcelBR.XmlLocation;
            excelFile = _generateExcelBR.excelFile;
            ProcessDate = _generateExcelBR.ProcessDate;
            pdate = _generateExcelBR.Pdate;
            PMonthlydate = _generateExcelBR.PMonthlydate;

        }
        public void OnGet(string category)
        {
            Categories = category;    
           // APP_Reportsx = _DbContext.APP_Reports.Where(r => r.Category == category).ToList();
            //APP_Reports = _niyi.APP_Report.Where(r => r.Category == category).ToList();
            APP_Reportsx = _DbContext.APP_Reports2.Where(r => r.Category == category);
           // Departments = APP_Reportsx.Select(x => x.Department.ToUpper()).ToList();

            Reports = _DbContext.APP_Reports2.ToList();
            filteredReports = _DbContext.APP_Reports2
           .Where(report => report.Category == category)
           .ToList();
            Departments = filteredReports.Select(x => x.Department.ToUpper()).ToList();



        }

        //public void OnPost(string category)
        //{
        //    APP_Reportsx = _DbContext.APP_Reports2.Where(r => r.Category == category);
        //    var is_available = category;
        //    Console.WriteLine(is_available);



        //}


        public async Task<IActionResult> OnPostAsync(string category)
        {
            ResultMessages = new List<string>();
            PMonthlydate = _generateExcelBR.PMonthlydate;
            categoryValue = category;

            if (category == "DBR" || category == "MBR" || category == "QBR" || category == "SBR")
            {
                XmlLocation = _generateExcelBR.XmlLocation;
                excelFilePath = _generateExcelBR.excelFilePath;
                pStartDate = _generateExcelBR.PStartDate;
                pEndDate = _generateExcelBR.PEndDate;
                subfolderPath = _parameterCalls.CreateFolderAndTextFile(PMonthlydate, category, XmlLocation);
            }
            else if (category == "MTR" || category == "DTR")
            {
                XmlLocation = _generateExcelBR.XmlLocation;
                excelFilePath = _generateExcelBR.excelFilePath;
                pStartDate = _generateExcelBR.PStartDate;
                pEndDate = _generateExcelBR.PEndDate;
                subfolderPath = _parameterCalls.CreateFolderAndTextFile(PMonthlydate, category, XmlLocation);

            }

            else if (category == "CRD")
            {
                XmlLocation = _generateExcelBR.XmlLocation;
                excelFilePath = _generateExcelBR.excelFilePath;
                pStartDate = _generateExcelBR.PStartDate;
                pEndDate = _generateExcelBR.PEndDate;
                subfolderPath = _parameterCalls.CreateFolderAndTextFile(PMonthlydate, category, XmlLocation);

            }
            else
            {
            }

            FieldSelector fieldSelector = new FieldSelector(category, excelFilePath, pStartDate, pEndDate, pdate);
            xmlReportStartDate = fieldSelector.xmlReportStartDate;
            xmlReportEndDate = fieldSelector.xmlReportEndDate;
            excelFile = fieldSelector.excelFile;
            Frequency = fieldSelector.Frequency;



            Reports = _DbContext.APP_Reports2.ToList();
            filteredReports = _DbContext.APP_Reports2
           .Where(report => report.Category == category)
           .ToList();

            Departments = filteredReports.Select(x => x.Department.ToUpper()).ToList();

            //subfolderPath = _parameterCalls.CreateFolderAndTextFile(PMonthlydate, category, XmlLocation);
            //excelFile = _generateExcelBR.GetExcelFile(category);
            //ar selectCountList=SelectedReports.Select(x => x.To == True).ToList();
            //SelectedCount=selectCountList.Count;
             SelectedCount = SelectedReports.Count(b => b);

            for (int i = 0; i < filteredReports.Count; i++)
            {
                if (SelectedReports[i])
                {
                    //SelectedReports[i].Select(x=>x==true).ToList();
                    //SelectedCount++;

                    // Execute the method for the selected report
                    switch (filteredReports[i].Report_ID)
                    {
                        case "MBR302" or "MBR304" or "MBR306" or "MBR308" or "MBR310" or "MBR312" or "MBR314" or "MBR316" or "MBR338" or "MBR342" or "MBR346"or
                        "MBR348" or "MBR350" or "MBR352"  or "MBR368" or "MBR370" or "MBR374" or "MBR376" or "MBR378" or "MBR380" or "MBR384" or
                         "MBR386" or "MBR390" or "MBR394.1" or "MBR394.2" or "MBR394.3" or "MBR394.4" or "MBR394.5" or
                        "MBR380" or "MBR390" or "DBR354" or "MBR352" 
                          or "MBR356" or "MBR358"  or "MRB316" or "MBR344" or "MBR46" or "MBR372" or "MBR382" or
                        "MBR387" or "MBR388" or "MBR389" or "MBR396" or "MBR398" or "MBR400" or "MBR402" or "MBR404" or "MBR406" or "MBR408"  or
                         "MBR358"   or "DBR302" or "DBR304" or "DBR306" or "DBR308" or "DBR310" or "DBR312" or "DBR314" or "DBR316"or "DBR338" or
                        "DBR342" or "DBR344" or "DBR346" or "DBR348" or "DBR350" or "DBR352" or  "DBR356" or "DBR358"  or "DBR364" or "DBR368" or "DBR370" or
                        "DBR372" or "DBR374" or "DBR376" or "DBR378" or "DBR380" or "DBR382" or "DBR384" or "DBR386" or
                        "DBR387" or "DBR388" or "DBR389" or "DBR390" or "DBR396"  or "DBR400" or "DBR404": 
                         


                            string result = await _generateExcelBR.ExcelBRGroup1(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            //_reportMethods.Method2()
                            break;
                        case "MBR318.1" or "MBR320.1" or "MBR322.1" or "MBR324.1" or "MBR326.1" or "MBR328.1" or "MBR330.1" or
                             "MBR332.1" or "MBR334.1" or "MBR336.1" or
                             "MBR340.1" or "MBR356" or "MBR358"  or "MBR340.1" or "MBR392.1" or "DBR318.1" or "DBR320.1" or "DBR322.1" or "DBR324.1" or
                             "DBR326.1" or "DBR328.1" or "DBR330.1" or "DBR332.1" or "DBR334.1" or "DBR336.1"  or "DBR340.1" or "DBR392.1":


                            result = await _generateExcelBR.ExcelBRGroupT1(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10

                        case "MBR354" or "MSR500"  or"MSR318" or "MSR354" or "MSR356" or "MSR362":


                            result = await _generateExcelBR.ExcelBRGroup1X(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10

                        case "MBR364":


                            result = await _generateExcelBR.ExcelBRGroup364(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10

                        case "MSR398":


                            result = await _generateExcelBR.ExcelBRGroupMSR398(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10

                        case "MBR360_1" or "MBR360_2" or "MBR360_3" or "MBR360_4" or "DBR360_1" or "DBR360_2" or "DBR360_3" or "DBR360_4":


                            result = await _generateExcelBR.ExcelBRGroup1a(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10

                        case "MBR318.2" or "MBR320.2" or "MBR322.2" or "MBR324.2" or "MBR326.2" or "MBR328.2" or "MBR330.2" or
                        "MBR332.2" or "MBR334.2" or "MBR336.2" or "MBR340.2" or "MBR392.2" or "DBR318.2" or "DBR320.2" or
                        "DBR322.2" or "DBR324.2" or "DBR326.2" or "DBR328.2" or "DBR330.2" or "DBR332.2" or "DBR334.2" or
                        "DBR336.2" or "DBR340.2" or "DBR392.2":



                            result = await _generateExcelBR.ExcelBRGroupT2(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;


                        // Implement cases for Method3 to Method10

                        case "MBR660":


                            result = await _generateExcelBR.ExcelBRGroup660(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10

                        case "MBR318.3" or "MBR320.3" or "MBR322.3" or "MBR324.3" or "MBR326.3" or "MBR328.3" or "MBR330.3" or
                        "MBR332.3" or "MBR334.3" or "MBR336.3" or
                        "MBR340.3" or "MBR392.3" or "DBR318.3" or "DBR320.3" or "DBR322.3" or "DBR324.3" or
                        "DBR326.3" or "DBR328.3" or "DBR330.3" or "DBR332.3" or "DBR334.3" or "DBR336.3" or "DBR340.3" or "DBR392.3":


                            result = await _generateExcelBR.ExcelBRGroupT3(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10
                        case "MSR359" or "MSR361" or "MSR360" or "MSR338" or "MSR320" or "MSR339" or "MSR340" or "MSR341"  or "MSR352" or "MSR353" or
                          "MBR600" or "MBR620"  or "MBR780" or "MSR345" or
                         "MSR255" or "MSR366" or "MSR394" or "MSR395" or "MSR318"  or "MSR392" or "MSR367" or "MSR392" or "MSR398" or "MSR402" or "MSR404" or
                        "MSR406" or "MSR408" or "MSR502" or "MSR504" or "MSR508" or "MSR600"  or "MSR358":


                            result = await _generateExcelBR.ExcelBRGroupGroup2(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10
                        case "MBR540a" :


                            result = await _generateExcelBR.ExcelBRGroupGroup540(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;


                        // Implement cases for Method3 to Method10
                        case "MBR580" or "MBR740" or "MBR840" or "MBR540":

                            result = await _generateExcelBR.ExcelBRGroup580(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10
                        case  "MBR590" :


                            result = await _generateExcelBR.ExcelBRGroup590(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10
                        case  "MBR750" or "MBR760" or "MBR770" :


                            result = await _generateExcelBR.ExcelBRGroup750(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;


                        // Implement cases for Method3 to Method10
                        case "MBR510" :

                            result = await _generateExcelBR.ExcelBRGroup510(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10
                        case   "DBR394A"
:

                            result = await _generateExcelBR.ExcelBRGroupT03(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;


                        // Implement cases for Method3 to Method10

                        case "MBR570":

                            result = await _generateExcelBR.ExcelBRGroup570(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10

                        case "MBR500"  or "MBR550" or "MBR630" or "MBR680" or "MBR690" or "MBR700"  or "MBR720"  or
                             "MBR790" or "MBR800"  or "MBR820"  or "MBR855" or "MBR860" or "DBR500":

                            result = await _generateExcelBR.ExcelBRGroup500(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10

                        case "MBR710": 
                             
                            result = await _generateExcelBR.ExcelBRGroup710(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10

                        case "MBR520" :

                            result = await _generateExcelBR.ExcelBRGroup520(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10

                        case  "MBR850" :

                            result = await _generateExcelBR.ExcelBRGroup850a(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10

                        case "MBR810" :

                            result = await _generateExcelBR.ExcelBRGroup810(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10

                        case "MBR670":

                            result = await _generateExcelBR.ExcelBRGroup670(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;


                        // Implement cases for Method3 to Method10

                        case "MBR730":

                            result = await _generateExcelBR.ExcelBRGroup730(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10

                        case "MBR300" or "DBR300":

                            result = await _generateExcelBR.ExcelBRGroup300(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10

                        case "MBR610" or "MBR640" or "MBR650" or "MBR250":

                            result = await _generateExcelBR.ExcelBRGroup610(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        
                        // Implement cases for Method3 to Method10

                        case   "MBR394" or "DBR394" :
                            result = await _generateExcelBR.ExcelBRGroupT02c(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10

                        case  "MBR830":
                            result = await _generateExcelBR.ExcelBRGroup830(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10

                        case "MBR560" :
                            result = await _generateExcelBR.ExcelBRGroup560(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;



                        // Implement cases for Method3 to Method10

                        case "MBR360" or "DBR360":
                            result = await _generateExcelBR.ExcelBRGroup360(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10

                        case  "MBR1006":
                            result = await _generateExcelBR.ExcelBRGroupMBR1006(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10

                        case "MBR1002":
                            result = await _generateExcelBR.ExcelBRGroupMBR1002(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10

                        case "MBR1000":
                            result = await _generateExcelBR.ExcelBRGroupMBR1000(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10

                        case "MBR1010" or "MBR1020" :
                                    result = await _generateExcelBR.ExcelBRGroupT02d(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                                    ResultMessages.Add(result);
                                    break;
                                // Implement cases for Method3 to Method10

                                case "MBR362" or "MBR366"  or "DBR362" or "DBR366":
                            result = await _generateExcelBR.ExcelBRGroupT02b(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10

                        case "MBR1004" or "MBR1008" or "MBR1012" or "MBR1014" or "MBR1016" or "MBR1018" or "MBR1022":

                            result = await _generateExcelBR.ExcelBRGroupT0(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10

                        case "MBR394_1" or "MBR394_2" or "MBR394_3" or "MBR394_4" or "MBR394_5" or "DBR394_1" or "DBR394_2" or "DBR394_3" or "DBR394_4" or "DBR394_5":

                            result = await _generateExcelBR.ExcelBRGroup394(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10 ExcelBRGroupQBR1

                        case "QBR1810" or "QBR1830" or "QRL1320" or "QRL1350" or "QRL1370" or "QRL1390" or "QBR1820" or
                        "QRL1310" or "QRL1340"  or "QRL1380" or "QRL1400" or "QRR1300" or "QRR1410" or "QRR1420":

                            result = await _generateExcelBR.ExcelBRGroupQBR1(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;

                        // Implement cases for Method3 to Method10 ExcelBRGroupQBR1

                        case  "QRL1360":
                       

                            result = await _generateExcelBR.ExcelBRGroupQBR1360(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10 ExcelBRGroupQBR1

                        case "QBR1831":

                            result = await _generateExcelBR.ExcelBRGroupQBR2(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10 ExcelBRGroupQBR1

                        case "QRL1330":  

                            result = await _generateExcelBR.ExcelBRGroupQBR1330(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10 ExcelBRGroupQBR1

                        case "SBR1910.1":

                            result = await _generateExcelBR.ExcelBRGroupSBRT1(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10 ExcelBRGroupQBR1
                        case "SBR1910.2":

                            result = await _generateExcelBR.ExcelBRGroupSBRT2(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10 ExcelBRGroupQBR1

                        case "SBR1910.3":

                            result = await _generateExcelBR.ExcelBRGroupSBR19103(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10 ExcelBRGroupQBR1 


                        case "SBR1910.4":

                            result = await _generateExcelBR.ExcelBRGroupSBR19104(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                        // Implement cases for Method3 to Method10 ExcelBRGroupQBR1 

                        case "SBR1900" or"SBR1920"or"SBR1930"or"SBR1950"or"SBR1951"or "SBR1940"
:

                            result = await _generateExcelBR.ExcelSBRGroup1(subfolderPath, excelFile, Frequency, xmlReportStartDate, xmlReportEndDate, filteredReports[i].Report_ID);
                            ResultMessages.Add(result);
                            break;
                            

                    }
                }
            }
             ReportNum = ResultMessages.Count();
            //ViewData["RecordCount"] = 5;
            //ViewData["ResultMessages"] = ResultMessages;
            // Redirect to the same page after executing methods
            return Page();
        }

    }
}

