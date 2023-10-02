using Microsoft.AspNetCore.Mvc;
using WEB_API_DataServer.Models.Account;
using API_Classes;

namespace WEB_API_DataServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        [HttpGet]
        [Route("values/{index}")]
        public IActionResult EntryByIndex(int index)
        {
            Account account = Database.GetAcctByIndex(index);
            DataIntermed data = new DataIntermed();

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                data.acct = account.acctNo;
                data.lname = account.lastName;
                data.fname = account.firstName;
                data.pin = account.pin;
                data.bal = account.balance;

                return Ok(data);
            }
        }

        [HttpPost]
        [Route("search")]
        public IActionResult EntryBySurname([FromBody]SearchData searchData)
        {
            Account account = Database.GetAcctBySurname(searchData.searchStr);
            DataIntermed data = new DataIntermed();

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                data.acct = account.acctNo;
                data.lname = account.lastName;
                data.fname = account.firstName;
                data.pin = account.pin;
                data.bal = account.balance;

                return Ok(data);
            }
        }

        [HttpGet]
        [Route("total")]
        public IActionResult NumEntries()
        {
            return Ok(Database.GetNumRecords());
        }
    }
}