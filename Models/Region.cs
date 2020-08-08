using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace HospitalTECnologico.Models
{
    public class Region
    {
        public int IdRegion { get; set; }
        public string Nombre { get; set; }
        public int Pais { get; set; }
    }

    public class createRegion : Region
    {
    }

    public class readRegion : Region
    {
        public readRegion(DataRow row)
        {
            IdRegion = Convert.ToInt32(row["IdRegion"]);
            Nombre = row["Nombre"].ToString();
            Pais = Convert.ToInt32(row["idPais"]);
        }
    }
}