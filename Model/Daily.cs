﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Model
{
    public class DailyTable
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public float Rate { get; set; }

    }
}
