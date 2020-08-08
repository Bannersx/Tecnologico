using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HospitalTECnologico.Models
{
    public class Cama
    {
        public int IdCama { get; set; }
        public int Salon { get; set; }
        public bool UCI { get; set; }
        public bool Libre { get; set; }
    }

    public class createCama : Cama
    {
    }

    public class readCama : Cama
    {
        public readCama(DataRow row)
        {
            IdCama = Convert.ToInt32(row["IdCama"]);
            Salon = Convert.ToInt32(row["Salon"]);
            UCI = Convert.ToBoolean(row["UCI"].ToString());
            Libre = Convert.ToBoolean(row["Libre"].ToString());

        }
    }
}