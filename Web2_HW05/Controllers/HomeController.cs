using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Refit;
using ServiceReference;
using Web2_HW05.Models;

namespace Web2_HW05.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IActionResult> Index()
        {
            var client = new NumberConversionSoapTypeClient(NumberConversionSoapTypeClient.EndpointConfiguration.NumberConversionSoap);
            var dollars = await client.NumberToDollarsAsync(12345.67M);
            ViewData["dollars"] = dollars.Body.NumberToDollarsResult;


            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://api.walmartlabs.com/v1/search?query=ipod&format=json&apiKey=mb94unfjgxety22grbux6zb4");
            var walmartSearchResult = new
            {
                Query = "",
                TotalResults = 1,
                Start=1,
                NumItems=1,
                Items=new[]{
                    new {
                        ItemId=1,
                        Name="",
                        Msrp=1M,
                        SalePrice=1M,
                        Upc="",
                        CategoryPath="",
                        ShortDescription="",
                        ThumbnailImage=""
                    }
                }
            };
            var searchResults = JsonConvert.DeserializeAnonymousType(json, walmartSearchResult);

            var walmartApi = RestService.For<IWalmartSearchApi>("http://api.walmartlabs.com");
            var walmartResults = await walmartApi.SearchAsync("maple syrup", configuration["keys:walmart"]);
            ViewData["walmartResults"] = walmartResults;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
