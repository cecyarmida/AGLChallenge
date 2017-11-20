using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using CallJson.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CallJson.Controllers
{
    public class HomeController : Controller
    {
        public static IConfigurationRoot Configuration { get; set; }
        public async Task<IActionResult> Index()
        {
            List<PersonPet> personPets = null;
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            var apiUrl = Configuration["FunctionURL"].ToString();

            try
            {
                string json = await GetJson(apiUrl);
                JArray array = JArray.Parse(json);
                personPets = array.ToObject<List<PersonPet>>();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return View(personPets);
        }

        private async Task<string> GetJson(string apiURL)
        {
            
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(apiURL));
           

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.SendAsync(request);
            string responseString = response.Content.ReadAsStringAsync().Result.ToString();

           

            return responseString;



        }
    }
}