using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HospitalTECnologico.Models
{
    public class Personal
    {
        public int idPersonal { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string telefono { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int direccion { get; set; }
        public int Tipo { get; set; }

    }
    public class createPersonal : Personal
    {
    }

    public class readPersonal : Personal
    {
        public readPersonal(DataRow row)
        {
            idPersonal = Convert.ToInt32(row["idpersonal"]);
            nombre = row["nombre"].ToString();
            apellido1 = row["apellido1"].ToString();
            apellido2 = row["apellido2"].ToString();
            telefono = row["telefono"].ToString();
            fechaNacimiento = Convert.ToDateTime(row["fechanacimiento"].ToString());
            direccion = Convert.ToInt32(row["direccion"]);
            Tipo = Convert.ToInt32(row["tipo"]);



        }
    }
}