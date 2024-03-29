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
    public class EquipomedicoController : ApiController
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
            string sql = "SELECT * FROM equipomedico";
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

            List<HospitalTECnologico.Models.Equipomedico> patologias = new List<HospitalTECnologico.Models.Equipomedico>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow patologiaRecord in dt.Rows)
                {
                    patologias.Add(new readEquipomedico(patologiaRecord));
                }
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(patologias));
            return response;
        }

        // GET: api/Test/5
        [Route("api/Equipomedico/{id:int}")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            // PostgeSQL-style connection string
            string connstring = String.Format("Server = tecnologicodb.postgres.database.azure.com; Database =postgres; Port = 5432; User Id = alex@tecnologicodb; Password =tecnologico123!; Ssl Mode = Require;");
            // Making connection with Npgsql provider
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            // quite complex sql statement
            string sql = "SELECT * FROM equipomedico WHERE idequipomedico =" + id;
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

            List<HospitalTECnologico.Models.Equipomedico> patologias = new List<HospitalTECnologico.Models.Equipomedico>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow patologiaRecord in dt.Rows)
                {
                    patologias.Add(new readEquipomedico(patologiaRecord));
                }
            }



            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent(JsonConvert.SerializeObject(patologias));
            return response;
        }

        // POST: api/Test
        public HttpResponseMessage Post([FromBody] createEquipomedico value)
        {
            NpgsqlConnection connstring = new NpgsqlConnection("Server = tecnologicodb.postgres.database.azure.com; Database =postgres; Port = 5432; User Id = alex@tecnologicodb; Password =tecnologico123!; Ssl Mode = Require;");

            var query = "INSERT INTO public.equipomedico (idequipomedico, nombre, proveedor, disponibles) values(@idequipomedico, @nombre, @proveedor, @disponibles)";

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            //response.Content = new StringContent("{\"type\":\"" + value.Id + "\",\"success\":true}");
            //return response;

            NpgsqlCommand insertCommand = new NpgsqlCommand(query, connstring);
            insertCommand.Parameters.AddWithValue("@idequipomedico", value.IdEquipomedico);
            insertCommand.Parameters.AddWithValue("@nombre", value.Nombre);
            insertCommand.Parameters.AddWithValue("@proveedor", value.Proveedor);
            insertCommand.Parameters.AddWithValue("@disponibles", value.Disponbiles);
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
        [Route("api/Equipomedico/{id:int}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            //Iniatilizing the response message
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            //Connection String 
            NpgsqlConnection connect = new NpgsqlConnection("Server = tecnologicodb.postgres.database.azure.com; Database =postgres; Port = 5432; User Id = alex@tecnologicodb; Password =tecnologico123!; Ssl Mode = Require;");
            //Query String
            var query = "DELETE FROM public.equipomedico WHERE idequipomedico=" + id;
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