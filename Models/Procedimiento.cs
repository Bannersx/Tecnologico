using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HospitalTECnologico.Models
{
    public class Procedimiento
    {
        public int IdProcedimiento { get; set; }
        public string Nombre { get; set; }
        
        public int DiasRecuperacion { get; set; }
        public string Tratamiento { get; set; }

    }
    public class createProcedimiento : Procedimiento
    {
    }

    public class readProcedimiento : Procedimiento
    {
        public readProcedimiento(DataRow row)
        {
            IdProcedimiento = Convert.ToInt32(row["idpersonal"]);
            Nombre = row["nombre"].ToString();
            Tratamiento = row["Tratamiento"].ToString();
            DiasRecuperacion = Convert.ToInt32(row["dias"]);
            
        }
    }
}