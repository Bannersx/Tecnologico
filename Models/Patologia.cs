using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace HospitalTECnologico.Models
{
    public class Patologia
    {
        public int IdPatologia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
    }

    public class createPatologia : Patologia
    {
    }

    public class readPatologia : Patologia
    {
        public readPatologia(DataRow row)
        {
            IdPatologia = Convert.ToInt32(row["IdPatologia"]);
            Nombre = row["Nombre"].ToString();
            Descripcion = row["Descripcion"].ToString();
        }
    }
}