using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Pages.ControlPannel.SuperUser
{
    public class DateSettingConfigModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<DateSettings> DateConfigList { get; set; }
        public int Num { get; set; }
        public DateSettingConfigModel(ApplicationDbContext context)
        {
            _context=context;
        }
        public void OnGet()
        {
            DateConfigList = _context.DateSettings.ToList();
            Num = DateConfigList.Count();
        }

        public void OnPost()
        {
            var datePurpose = Request.Form["datePurpose"];
            var dateActual = Request.Form["dateActual"];
            var purposeDetails = Request.Form["purposeDetails"];
            DateTime actualDates;
            var isValidDate = DateTime.TryParse(dateActual, out actualDates);
            
            var id = Request.Form["id"];

            var dateConfigList = _context.DateSettings.Find(int.Parse(id));

            if (dateConfigList != null)
            {

                dateConfigList.DatePurpose = datePurpose;
                dateConfigList.DateActual = actualDates;
                dateConfigList.PurposeDetails = purposeDetails;

                _context.SaveChanges();
            }

            DateConfigList = _context.DateSettings.ToList();
            
        }
    }
}
