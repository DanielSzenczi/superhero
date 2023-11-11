using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace HeroQuotesFunction
{
    public static class HeroFunction
    {
        [FunctionName("GetSuperheroQuote")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetSuperheroQuote")] HttpRequest req,ILogger log)
        {
            log.LogInformation("Request for GetSuperheroQuote");

            return "dsad";
        }
    }
}
