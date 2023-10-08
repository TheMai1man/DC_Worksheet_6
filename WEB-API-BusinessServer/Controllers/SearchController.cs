using API_Classes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace WEB_API_BusinessServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        [HttpPost]
        [Route("surname")]
        public IActionResult EntryBySurname([FromBody] SearchData data)
        {
            RestClient restClient = new RestClient("http://localhost:5002");
            RestRequest restRequest = new RestRequest("/api/account/search", Method.Post);
            restRequest.RequestFormat = RestSharp.DataFormat.Json;
            restRequest.AddBody(data);

            RestResponse restResponse = restClient.Execute(restRequest);

            if(restResponse.IsSuccessful == false)
            {
                return NotFound();
            }

            return Ok(JsonConvert.DeserializeObject<DataIntermed>(restResponse.Content));
        }
    }
}