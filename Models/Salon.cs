using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HospitalTECnologico.Models
{
    public class Salon
    {
        public int IdSalon { get; set; }
        public string Nombre { get; set; }

        public int Capacidad { get; set; }
        public int Tipo { get; set; }
        public int Piso { get; set; }

    }
    public class createSalon : Salon
    {
    }

    public class readSalon : Salon
    {
        public readSalon(DataRow row)
        {
            IdSalon = Convert.ToInt32(row["idsalon"]);
            Nombre = row["nombre"].ToString();
            Capacidad = Convert.ToInt32(row["capacidad"]);
            Tipo = Convert.ToInt32(row["tipo"]);
            Piso = Convert.ToInt32(row["piso"]);


        }
    }
}