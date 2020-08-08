using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace HospitalTECnologico.Models
{
    public class HistorialProcedimiento
    {

        public string procedimiento { get; set; }
        public string tratamiento { get; set; }
        public DateTime Fecha { get; set; } //USAMOS STRING PARA LAS FECHAS?
    }

    public class createHistorialProcedimiento : HistorialProcedimiento
    {
    }

    public class readHistorialProcedimiento : HistorialProcedimiento
    {
        public readHistorialProcedimiento(DataRow row)
        {
            Fecha = Convert.ToDateTime(row["Fecha"].ToString());
            procedimiento = row["procedimiento"].ToString();
            tratamiento = row["tratamiento"].ToString();
            
        }
    }
}