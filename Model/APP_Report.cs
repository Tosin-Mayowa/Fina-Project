using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Model
{
    public class APP_Report
    {

        [Key]
        public int NO { get; set; }
        public string Report_ID { get; set; }
        public string Report_Description { get; set; }

        public string Category { get; set; }

        public string Frequency { get; set; }

        public string Department { get; set; }

        public string GMethod { get; set; }

        public int? Checkbox { get; set; }
        public string? Datalink1 { get; set; }
        public string? Datalink2 { get; set; }
    }
}
