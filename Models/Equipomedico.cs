using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace HospitalTECnologico.Models
{
    public class Equipomedico
    {
        public int IdEquipomedico { get; set; }
        public string Nombre { get; set; }
        public string Proveedor { get; set; }
        public int Disponbiles { get; set; }
    }

    public class createEquipomedico : Equipomedico
    {
    }

    public class readEquipomedico : Equipomedico
    {
        public readEquipomedico(DataRow row)
        {
            IdEquipomedico = Convert.ToInt32(row["IdEquipomedico"]);
            Nombre = row["Nombre"].ToString();
            Proveedor = row["Proveedor"].ToString();
            Disponbiles = Convert.ToInt32(row["Disponibles"]);
        }
    }
}