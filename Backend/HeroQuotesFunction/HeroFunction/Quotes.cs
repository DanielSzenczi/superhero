using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;
using HeroFunction.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace HeroFunction
{
    public class Quotes
    {
        private readonly ILogger _logger;

        public Quotes(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Quotes>();
        }



        [Function("GetSuperheroQuote")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetQuote")] HttpRequestData req)
        {
  
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            response.WriteString(GetRandomQuoteFromFile());
            return response;
        }


        /// <summary>
        /// Small helper method that reads from the json file and selects a random quote from the list
        /// </summary>
        /// <returns></returns>
        private string GetRandomQuoteFromFile()
        {

            string file = Path.Combine(Environment.CurrentDirectory, "Resources/quotes.json");
            string jsonContent = File.ReadAllText(file);

            List<SuperheroQuote> quoteList = JsonConvert.DeserializeObject<List<SuperheroQuote>>(jsonContent);

            Random random = new Random();
            int random_number = random.Next(quoteList.Count);

            string json = JsonConvert.SerializeObject(quoteList[random_number]);
            return json;

        }
    }
}
