using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Functions
{
    public static class JsonMapper
    {
        [FunctionName("JsonMapper")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string jsonURL = System.Environment.GetEnvironmentVariable("JsonURL");
            List<PersonCat> personCats = new List<PersonCat>();
            string error = null;

            try
            {
                var client = new WebClient();
                string json = await client.DownloadStringTaskAsync(jsonURL);

                var people = JsonConvert.DeserializeObject<List<Person>>(json)
                                         .Where(p => (p.Pets != null));
                people.ToList()
                       .ForEach(x => x.Pets.RemoveAll(c => c.Type != "Cat"));

                personCats = people
                    .GroupBy(a => a.Gender)
                    .Select(
                        x => new PersonCat
                        {
                            Gender = x.Key,
                            Cats = x.SelectMany(c => c.Pets).OrderBy(p => p.Name).ToList()
                        }).ToList();

            }
            catch (Exception ex)
            {
                var errorRes = new HttpResponseMessage(HttpStatusCode.BadRequest);
                errorRes.Content = new StringContent(ex.Message.ToString());
                return errorRes;
            }

            var content = personCats == null ? new StringContent(JsonConvert.SerializeObject(error)) : new StringContent(JsonConvert.SerializeObject(personCats));
            var res = new HttpResponseMessage(personCats == null ? HttpStatusCode.BadRequest : HttpStatusCode.OK);
            res.Content = content;
            return res;
        }
        public class Person
        {

            public string Name { get; set; }
            public string Gender { get; set; }
            public string Age { get; set; }

            public List<Pet> Pets { get; set; }
        }

        public class Pet
        {

            public string Name { get; set; }
            public string Type { get; set; }
        }

        public class PersonCat
        {
            public string Gender { get; set; }
            public List<Pet> Cats { get; set; }
        }
    }
}
