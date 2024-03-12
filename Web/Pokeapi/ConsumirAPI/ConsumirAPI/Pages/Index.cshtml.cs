using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ConsumirAPI.Model;
using System.Net.Http.Headers;

namespace ConsumirAPI.Pages
{
    public class IndexModel : PageModel
    {

        public List<WeatherForecast> climaList { get; set; }

        static HttpClient client = new HttpClient();

        static async Task<List<WeatherForecast>> RunAsync()
        {
            List<WeatherForecast> climaList = new List<WeatherForecast>();

            client.BaseAddress = new Uri("https://localhost:7002/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                               new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                //Path interno del end point
                HttpResponseMessage Res = await client.GetAsync("MaxTemperature");
                //Checar si el estatus es correcto del HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Obtener el response recibido web api
                    var apiResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing la respuesta del web api y guardarlo en la lista
                    climaList = JsonConvert.DeserializeObject<List<WeatherForecast>>(apiResponse);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return climaList;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            climaList = await RunAsync();
            return Page();
        }
    }
}
