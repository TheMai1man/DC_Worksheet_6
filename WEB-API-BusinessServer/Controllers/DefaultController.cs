using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace WEB_API_BusinessServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : Controller
    {
        [HttpGet]
        [Route("total")]
        public IActionResult TotalAccounts()
        {
            RestClient restClient = new RestClient("http://localhost:5002");
            RestRequest restRequest = new RestRequest("/api/account/total/", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);

            return Ok(JsonConvert.DeserializeObject<int>(restResponse.Content));
        }
    }
}