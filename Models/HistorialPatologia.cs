using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace HospitalTECnologico.Models
{
    public class HistorialPatologia
    {
       
        public int Paciente { get; set; }
        public int Patologia { get; set; }
        public DateTime Fecha { get; set; } //USAMOS STRING PARA LAS FECHAS?
    }

    public class createHistorialPatologia : HistorialPatologia
    {
    }

    public class readHistorialPatologia : HistorialPatologia
    {
        public readHistorialPatologia(DataRow row)
        {
            
            Paciente = Convert.ToInt32(row["idPaciente"]);
            Patologia = Convert.ToInt32(row["idPatologia"]);
            Fecha = Convert.ToDateTime(row["Fecha"].ToString());
        }
    }
}