using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ClassLibrary1;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityServerWebSite1.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


        [HttpGet("Contact")]
        [Authorize]
        public async Task<IActionResult> Contact()
        {
            var model = await GetCustomerAsync();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View();
        //}

        public async Task<CustomerViewModel> GetCustomerAsync()
        {
            var CustomerViewModel = new CustomerViewModel();
            using (var httpClient = new HttpClient())
            {
                string accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

                // httpClient.BaseAddress = new Uri("http://localhost:5001");
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var uri = string.Format("{0}/{1}",
                                  "http://localhost:5001",
                                  "api/clock/Customer");

                var results = await httpClient.GetAsync(uri).ConfigureAwait(false);

                if (results.IsSuccessStatusCode)
                {
                    var jsonString = await results.Content.ReadAsStringAsync();
                    CustomerViewModel = JsonConvert.DeserializeObject<CustomerViewModel>(jsonString);

                }
                return CustomerViewModel;
            }
        }
    }
}
