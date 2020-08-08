using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using HospitalTECnologico.Models;
using System.Globalization;
//using MongoDB.Driver.Builders;

namespace HospitalTECnologico.Controllers
{
    public class EvaluacionController : ApiController
    {
        // GET: api/Evaluacion
        [Route("api/Evaluacion")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            // Buils client, gets database and collection
            var client = new MongoClient("mongodb+srv://fabian:1234qwerty@proyecto2bd.rnwim.azure.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("hispitaltecnologico");
            var collection = database.GetCollection<BsonDocument>("evaluaciones");

            //Builds response
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            //Tries to find all documents
            try
            {
                var documents = collection.Find(new BsonDocument()).ToList();
                response.Content = new StringContent(JsonConvert.SerializeObject(documents));
            }
            catch
            {
                response.Content = new StringContent("{\"type\":\"post\",\"outcome\":success}");
            }
            return response;
        }

        // GET: api/Evaluacion/5
        [Route("api/Evaluacion/{paciente:int}")]
        [HttpGet]
        public HttpResponseMessage Get(int paciente)
        {
            // Buils client, gets database and collection
            var client = new MongoClient("mongodb+srv://fabian:1234qwerty@proyecto2bd.rnwim.azure.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("hispitaltecnologico");
            var collection = database.GetCollection<BsonDocument>("evaluaciones");

            //Builds response
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            //Builds filter
            var filter = Builders<BsonDocument>.Filter.Eq("Paciente", paciente);

            //Tries to find all documents that match pattern
            try
            {
                var documents = collection.Find(filter).FirstOrDefault();
                response.Content = new StringContent(JsonConvert.SerializeObject(documents));
            }
            catch
            {
                response.Content = new StringContent("{\"type\":\"post\",\"outcome\":failed}");
            }
            return response;
        }

        // GET: api/Evaluacion/Reporte
        [Route("api/Evaluacion/Reporte")]
        [HttpGet]
        public HttpResponseMessage GetReporte()
        {
            // Buils client, gets database and collection
            MongoClient client = new MongoDB.Driver.MongoClient("mongodb+srv://fabian:1234qwerty@proyecto2bd.rnwim.azure.mongodb.net/test?retryWrites=true&w=majority");
            IMongoDatabase database = client.GetDatabase("hispitaltecnologico");
            IMongoCollection<Evaluacion> collection = database.GetCollection<Evaluacion>("evaluaciones");

            //Generates sort order for query and empty filter
            var sort = Builders<Evaluacion>.Sort.Descending("_id");
            var filter1 = Builders<Evaluacion>.Filter.Empty;

            //Finds last 10 results from collection using sort and filter
            var results = collection.Find(filter1).Sort(sort).Limit(10).ToList();

            var aseo = 0;
            var tratoPersonal = 0;
            var puntualidad = 0;

            //Gets average of last 10 results
            foreach (var e in results)
            {
                aseo += e.Aseo;
                tratoPersonal += e.TratoPersonal;
                puntualidad += e.Puntualidad;
            }

            aseo /= 10;
            tratoPersonal /= 10;
            puntualidad /= 10;

            //Builds response message
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            //Decides if warning should be issued
            if (aseo <= 7 || tratoPersonal <= 7 || puntualidad <= 7)
            {
                response.Content = new StringContent("{\"type\":\"get\",\"alert\":true,\"aseo\":" + aseo + ",\"tratoPersonal\":" + tratoPersonal + ",\"puntualidad\"," + puntualidad + "}");
            }
            else
            {
                response.Content = new StringContent("{\"type\":\"get\",\"alert\":false,\"aseo\":" + aseo + ",\"tratoPersonal\":" + tratoPersonal + ",\"puntualidad\"," + puntualidad + "}");
            }
            return response;
        }

        // POST: api/Evaluacion
        [Route("api/Evaluacion")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] createEvaluacion evaluacion)
        {
            // Buils client, gets database and collection
            var client = new MongoClient("mongodb+srv://fabian:1234qwerty@proyecto2bd.rnwim.azure.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("hispitaltecnologico");
            var collection = database.GetCollection<BsonDocument>("evaluaciones");

            //Builds object from model
            var document = new BsonDocument {
                {"Paciente", evaluacion.Paciente},
                {"Aseo", evaluacion.Aseo},
                {"TratoPersonal", evaluacion.TratoPersonal},
                {"Puntualidad", evaluacion.Puntualidad},
                {"Fecha", evaluacion.Fecha.ToUniversalTime() }
            };

            //If theres a 'Detalle', it adds it to the object
            if (!String.IsNullOrEmpty(evaluacion.Detalle))
            {
                document.Add("Detalle", evaluacion.Detalle);
            }


            string status = "success";

            //Attempts to insert object
            try { collection.InsertOne(document); }
            catch (Exception e) { status = e.ToString(); }

            //Builds and returns status message
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new StringContent("{\"type\":\"post\",\"outcome\":" + status + "}");
            return response;
        }
    }
}