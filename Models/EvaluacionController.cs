using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace HospitalTECnologico.Models
{
    public class Evaluacion
    {
        // Evaluacion Object Model
        public ObjectId _id { get; set; }
        public int Paciente { get; set; }
        public int Aseo { get; set; }
        public int TratoPersonal { get; set; }
        public int Puntualidad { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class createEvaluacion : Evaluacion
    {
    }

    public class readEvaluacion : Evaluacion
    {
        public readEvaluacion(DataRow row)
        {
        }
    }
}