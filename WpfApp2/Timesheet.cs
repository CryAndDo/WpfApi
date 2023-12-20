using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Timesheet
    {
        public int Id { get; set; }
        public string? FIO { get; set; }
        public int Speciality_code { get; set; }
        public string? Workshop_name { get; set; }
        public string? Speciality { get; set; }
        public int Number_of_days_worked { get; set; }
        public double Zarplata { get; set; }
        public double Retention { get; set; }
        public double Amount_due { get; set; }
    }
}
