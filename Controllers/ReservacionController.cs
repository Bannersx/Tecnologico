using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Npgsql;
using System.Data;
using Newtonsoft.Json;
using HospitalTECnologico.Models;

namespace HospitalTECnologico.Controllers
{
    public class ReservacionController : ApiController
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        // GET api/values
        public HttpResponseMessage Get()
        {


            // PostgeSQL-style connection string
            string connstring = String.Format("Server=localhost;Port=5432;" +
                "User Id=postgres;Password=1234;Database=TecNologicoDB;");
            // Making connection with Npgsql provider
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            // quite complex sql statement
            string sql = "SELECT * FROM reservacion ORDER BY fechaingreso DESC;";
            // data adapter making request from our connection
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            // i always reset DataSet before i do
            // something with it.... i don't know why :-)
            ds.Reset();
            // filling DataSet with result from NpgsqlDataAdapter
            da.Fill(ds);
            // since it C# DataSet can handle multiple tables, we will select first
            dt = ds.Tables[0];
            // since we only showing the result we don't need connection anymore
            conn.Close();

            List<HospitalTECnologico.Models.Reservacion> records = new List<HospitalTECnologico.Models.Reservacion>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow Record in dt.Rows)
                {
                    records.Add(new readReservacion(Record));
                }
            }



            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(records));
            return response;
        }

        // GET api/values/5
        [Route("api/Reservacion/{id:int}")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {

            // PostgeSQL-style connection string
            string connstring = String.Format("Server=localhost;Port=5432;" +
                "User Id=postgres;Password=1234;Database=TecNologicoDB;");
            // Making connection with Npgsql provider
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            // quite complex sql statement
            string sql = "SELECT * FROM reservacion WHERE idpaciente ="+id+" ORDER BY fechaingreso DESC;";
            // data adapter making request from our connection
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            // i always reset DataSet before i do
            // something with it.... i don't know why :-)
            ds.Reset();
            // filling DataSet with result from NpgsqlDataAdapter
            da.Fill(ds);
            // since it C# DataSet can handle multiple tables, we will select first
            dt = ds.Tables[0];
            // since we only showing the result we don't need connection anymore
            conn.Close();

            List<HospitalTECnologico.Models.Reservacion> patologias = new List<HospitalTECnologico.Models.Reservacion>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow patologiaRecord in dt.Rows)
                {
                    patologias.Add(new readReservacion(patologiaRecord));
                }
            }



            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(patologias));
            return response;
        }

        // POST: api/Values
        public HttpResponseMessage Post([FromBody] createReservacion value)
        {
            NpgsqlConnection connstring = new NpgsqlConnection("Server=localhost;Port=5432;" +
                "User Id=postgres;Password=1234;Database=TecNologicoDB;");

            int size = value.Procedimientos.Length;

            value.procs += '{';
            int n = 0;

            foreach (int procedimiento in value.Procedimientos)
            {
                if (n != value.Procedimientos.Length)
                {
                    value.procs += procedimiento + ',';
                    n += 1;
                }
                else
                {
                    value.procs += procedimiento + '}';
                    n += 1;
                }
                
            }

            var query = "SELECT nueva_reservacion(@fechaentrada,@idpaciente, @procedimientos, @iddecama)";

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            //response.Content = new StringContent("{\"type\":\"" + value.Id + "\",\"success\":true}");
            //return response;

            NpgsqlCommand insertCommand = new NpgsqlCommand(query, connstring);
            insertCommand.Parameters.AddWithValue("@fechaentrada", value.FechaIngreso.Date);
            insertCommand.Parameters.AddWithValue("@idpaciente", value.idPaciente);
            insertCommand.Parameters.AddWithValue("@procedimientos", value.Procedimientos);
            insertCommand.Parameters.AddWithValue("@iddecama", value.idCama);
            //TRY-CATCH HERE TO CATCH DB ERRRORS!
            try
            {
                connstring.Open();
                int result = insertCommand.ExecuteNonQuery();
                response.Content = new StringContent("{\"type\":\"post\",\"success\":true}");
                return response;
            }
            catch (NpgsqlException pex) {
                string errmsg = pex.Message;
                response.Content = new StringContent("{\"type\":\"post\",\"success\":false}");
                return response;
                throw new Exception(errmsg);
            }
            
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
