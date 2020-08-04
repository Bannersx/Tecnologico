using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace HospitalTECnologico.Models
{
    public class Reservacion
    {

        public int idreservacion { get; set; }
        public int idPaciente { get; set; }
        public DateTime FechaIngreso { get; set; } //USAMOS STRING PARA LAS FECHAS?
        public DateTime FechaSalida { get; set; }
        public int idCama { get; set; }

        public int[] Procedimientos { get; set; }
        public string procs { get; set; }


    }

    public class createReservacion : Reservacion
    {
    }

    public class readReservacion : Reservacion
    {
        public readReservacion(DataRow row)
        {
            idreservacion = Convert.ToInt32(row["idreservacion"]);
            FechaIngreso = Convert.ToDateTime(row["fechaingreso"].ToString());
            FechaSalida = Convert.ToDateTime(row["fechasalida"].ToString());
            idCama = Convert.ToInt32(row["idcama"]);
            

        }
    }
}