using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalTECnologico.Models
{
    public class DateProcedure
    {
        public DateTime FechaEntrada { get; set; }
        public int[] procedimientos { get; set; }
        public string procedures { get; set; }
    }
    public class createDateProcedure : DateProcedure
    {

    }
}