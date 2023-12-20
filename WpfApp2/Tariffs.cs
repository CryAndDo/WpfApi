using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Tariffs
    {

        public int Speciality_code { get; set; }

        public string? Speciality_name { get; set; }
    
        public double Price { get; set; }
    }
}
