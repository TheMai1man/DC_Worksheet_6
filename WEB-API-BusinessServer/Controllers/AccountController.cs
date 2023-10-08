using API_Classes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace WEB_API_BusinessServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        [HttpGet]
        [Route("values/{index}")]
        public IActionResult EntryByIndex(int index)
        {
            RestClient restClient = new RestClient("http://localhost:5002");
            RestRequest restRequest = new RestRequest("/api/account/values/{index}", Method.Get);
            restRequest.AddUrlSegment("index", index);
            RestResponse restResponse = restClient.Execute(restRequest);

            DataIntermed data = JsonConvert.DeserializeObject<DataIntermed>(restResponse.Content);

            if (restResponse.IsSuccessful == false)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}