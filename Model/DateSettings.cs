using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Model
{
    public class DateSettings
    {
        public int Id { get; set; }
        public string DatePurpose { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateActual { get; set; }
        public string PurposeDetails { get; set; }
    }
}
