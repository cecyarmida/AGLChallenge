using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using CallJson.Models;

namespace CallJson.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<PersonPet> personPets = null;
            try
            {
                string json = await GetJson();
                JArray array = JArray.Parse(json);
                personPets = array.ToObject<List<PersonPet>>();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return View(personPets);
        }

        private async Task<string> GetJson()
        {
            var apiURL = "https://aglmapjason.azurewebsites.net/api/JsonMapper?code=e6ImfcGcYzhoxBAvjrMyGiLlPaOwDN7YIVRKe128Av0PAh2PQuGRaQ==";
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(apiURL));
           

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.SendAsync(request);
            string responseString = response.Content.ReadAsStringAsync().Result.ToString();

           

            return responseString;



        }
    }
}