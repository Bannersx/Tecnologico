using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace HospitalTECnologico.Models
{
    public class Paciente
    {
        public int Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public DateTime FechaNacimiento { get; set; } //USAMOS STRING PARA LAS FECHAS?
        public int Direccion { get; set; }
        
        public string Telefono { get; set; }
        

    }

    public class createPaciente : Paciente
    {
    }

    public class readPaciente : Paciente
    {
        public readPaciente(DataRow row)
        {
            Identificacion = Convert.ToInt32(row["IdPaciente"]);
            Nombre = row["Nombre"].ToString();
            Apellido1 = row["Apellido1"].ToString();
            Apellido2 = row["Apellido2"].ToString();
            Telefono = row["telefono"].ToString();
            Direccion = Convert.ToInt32(row["direccion"]);
            FechaNacimiento = Convert.ToDateTime(row["fechanacimiento"].ToString());
            
            
        }
    }
}