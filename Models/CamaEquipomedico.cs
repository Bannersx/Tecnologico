using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HospitalTECnologico.Models
{
    public class CamaEquipomedico
    {
        public int IdCama { get; set; }
        public int IdEquipomedico { get; set; }
    }

    public class createCamaEquipomedico : CamaEquipomedico
    {
    }

    public class readCamaEquipomedico : CamaEquipomedico
    {
        public readCamaEquipomedico(DataRow row)
        {
            IdCama = Convert.ToInt32(row["IdCama"]);
            IdEquipomedico = Convert.ToInt32(row["IdEquipomedico"]);
        }
    }
}