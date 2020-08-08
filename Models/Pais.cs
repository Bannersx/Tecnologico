using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace HospitalTECnologico.Models
{
    public class Pais
    {
        public int IdPais { get; set; }
        public string Nombre { get; set; }
    }

    public class createPais : Pais
    {
    }

    public class readPais : Pais
    {
        public readPais(DataRow row)
        {
            IdPais = Convert.ToInt32(row["IdPais"]);
            Nombre = row["Nombre"].ToString();
        }
    }
}