using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HospitalTECnologico.Models
{
    public class Camadisponible
    {
        public int IdCama { get; set; }
        
    }

    public class createCamadisponible : Camadisponible
    {
    }

    public class readCamadisponible : Camadisponible
    {
        public readCamadisponible(DataRow row)
        {
            IdCama = Convert.ToInt32(row["IdCama"]);
            

        }
    }
}