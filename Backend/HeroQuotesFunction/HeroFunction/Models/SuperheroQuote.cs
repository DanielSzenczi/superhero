using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroFunction.Models
{
    public class SuperheroQuote
    {
        [JsonProperty("quote")]
        public string Quote { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
