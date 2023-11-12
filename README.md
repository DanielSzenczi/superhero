# superhero

### Løsningen består af to dele:
* VUE 3 Single Page Application - Frontend 
* C# Azure Functions - Backend.

#### Frontend
VUE appen har en enkelt komponent ved navn **SuperHeroQuotes**, som har 2 attributter: *hero* og *hero_quote*. Derudover har den også en enkelt metode :**GetQuotes**.
```js
  export default {
    name: 'SuperHeroQuotes',
  data() {
    return {
      hero : "",
      hero_quote: "",
    };
  },
```

Ved initialisering (Mounting) af komponenten sættes der et interval på 10000 millisekunder (10 sek.) 
- Hvert 10. sekund kaldes GetQuotes metoden, som laver et GET request til Backenden, som returnerer et JSON objekt med et tilfældigt citat og den tilhørende superhelts navn.
- Resultat bliver derefter assignet til de 2 attributter og bliver vist til brugeren.
- 
#### Backend
Projekt struktur:
![[Pasted image 20231112111355.png]]

* Data model
```C#
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
```

* Azure Function med HTTP Trigger (.net 06)
	* Exposer en endpoint *"api/GetQuote".*
	* Returnerer et JSON string.
```C#
     [Function("GetSuperheroQuote")]
     public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous,"get", 
     Route = "GetQuote")] HttpRequestData req)
     {
         var response = req.CreateResponse(HttpStatusCode.OK);
         response.Headers.Add("Content-Type", "application/json");
         response.WriteString(GetRandomQuoteFromFile());
         return response;
     }
```

* Hjælpemetode ved navn *GetRandomQuoteFromFile* som læser fra JSON filen og vælger en tilfældig helt og dennes citat.
```C#
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
```

#### Fremtidige overvejelser/Forbedringer
Løsningen er relativt simpel og kunne godt drage nytte af flere exception handlinger.
* Et eksempel på dette kunne være, hvis filen ikke kan læses, eller noget går galt, så skal programmet håndtere dette.


##### Storage
*Azure Blob Storage* kunne være en mulighed for at gemme citater i skyen.
Her kunne man bruge en enkelt container som indeholder forskellige JSON filer med citater fra de forskellige superhelte eller en container per superhelt og deres citater.

Eksempel på det sidste:
* Container : "Batman"
	* GothamGuard.json

Alternativt kunne *Table Storage* blive brugt til at gemme enkelte række af citater samt heltens navn. En ulempe ved Table Storage er dog at komplekse søgninger er besværlige og kræver et unikt Partition Key og Row Key kombination-design for at kunne eksempelvis søge hvilken citat tilhører hvilken helt.

Uanset hvilken metode man går med for at gemme citater, burde et nyt REST API eller Azure Function endpoint udvikles, som gør det muligt at tilføje citater løbende, enten fra VUE appen eller fra andre systemer. 



##### Superhelt systemer
Hvis man ønsker forskellige systemer, som kan hjælpe med at identificere, hvilken helt har sagt hvilket citat, og hvor alle systemer er ens i funktionalitet ud over helten, de prøver at identificere, kunne en generisk arkitektur være en mulighed.

Modelen viser:
IQuoter interface med metoden identify
To eller flere klasser som implementerer interfacet med hver deres superheltenavn, de søger efter.
En QuoterService klasse som bruger dependency injection til at få fat på de forskellige IQuoter implementationer. 

![[SuperheltSystem.png]]
Denne tilgang gør det nemt at tilføje flere services, som identificerer citater, så længe de overholder interface og resten af strukturen.





