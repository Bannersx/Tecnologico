﻿using System;
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
    public class PersonalController : ApiController
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        // GET: api/Test
        public HttpResponseMessage Get()
        {


            // PostgeSQL-style connection string
            string connstring = String.Format("Server = tecnologicodb.postgres.database.azure.com; Database =postgres; Port = 5432; User Id = alex@tecnologicodb; Password =tecnologico123!; Ssl Mode = Require;");
            // Making connection with Npgsql provider
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            // quite complex sql statement
            string sql = "SELECT * FROM personal";
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

            List<HospitalTECnologico.Models.Personal> patologias = new List<HospitalTECnologico.Models.Personal>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow patologiaRecord in dt.Rows)
                {
                    patologias.Add(new readPersonal(patologiaRecord));
                }
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(patologias));
            return response;
        }

        // GET: api/Test/5
        [Route("api/Personal/{id:int}")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            // PostgeSQL-style connection string
            string connstring = String.Format("Server = tecnologicodb.postgres.database.azure.com; Database =postgres; Port = 5432; User Id = alex@tecnologicodb; Password =tecnologico123!; Ssl Mode = Require;");
            // Making connection with Npgsql provider
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            // quite complex sql statement
            string sql = "SELECT * FROM personal WHERE idpersonal =" + id;
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

            List<HospitalTECnologico.Models.Personal> patologias = new List<HospitalTECnologico.Models.Personal>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow patologiaRecord in dt.Rows)
                {
                    patologias.Add(new readPersonal(patologiaRecord));
                }
            }



            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(patologias));
            return response;
        }

        // POST: api/Test
        public HttpResponseMessage Post([FromBody] createPersonal value)
        {
            NpgsqlConnection connstring = new NpgsqlConnection("Server = tecnologicodb.postgres.database.azure.com; Database =postgres; Port = 5432; User Id = alex@tecnologicodb; Password =tecnologico123!; Ssl Mode = Require;");

            var query = "INSERT INTO public.personal (idpersonal, nombre, apellido1, apellido2, telefono, fechanacimiento, direccion, tipo) values(@idpersonal, @nombre, @apellido1, @apellido2, @telefono, @fechanacimiento, @direccion, @tipo)";

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            //response.Content = new StringContent("{\"type\":\"" + value.Id + "\",\"success\":true}");
            //return response;

            NpgsqlCommand insertCommand = new NpgsqlCommand(query, connstring);
            insertCommand.Parameters.AddWithValue("@idpersonal", value.idPersonal);
            insertCommand.Parameters.AddWithValue("@nombre", value.nombre);
            insertCommand.Parameters.AddWithValue("@apellido1", value.apellido1);
            insertCommand.Parameters.AddWithValue("@apellido2", value.apellido2);
            insertCommand.Parameters.AddWithValue("@telefono", value.telefono);
            insertCommand.Parameters.AddWithValue("@fechanacimiento", value.fechaNacimiento);
            insertCommand.Parameters.AddWithValue("@direccion", value.direccion);
            insertCommand.Parameters.AddWithValue("@tipo", value.Tipo);
            connstring.Open();
            int result = insertCommand.ExecuteNonQuery();
            if (result > 0)
            {
                response.Content = new StringContent("{\"type\":\"post\",\"success\":true}");
                return response;
            }
            else
            {
                response.Content = new StringContent("{\"type\":\"post\",\"success\":false}");
                return response;
            }
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Patologia/5
        [Route("api/Personal/{id:int}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            //Iniatilizing the response message
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            //Connection String 
            NpgsqlConnection connect = new NpgsqlConnection("Server = tecnologicodb.postgres.database.azure.com; Database =postgres; Port = 5432; User Id = alex@tecnologicodb; Password =tecnologico123!; Ssl Mode = Require;");
            //Query String
            var query = "DELETE FROM public.personal WHERE idpersonal=" + id;
            //Creating the sql command
            NpgsqlCommand DeleteCommand = new NpgsqlCommand(query, connect);
            //Opening the connection
            connect.Open();
            //Checking if the query succeeds or not
            int result = DeleteCommand.ExecuteNonQuery();
            if (result > 0)
            {
                response.Content = new StringContent("{\"type\":\"post\",\"success\":true}");
                return response;
            }
            else
            {
                response.Content = new StringContent("{\"type\":\"post\",\"success\":false}");
                return response;
            }

        }
    }
}
