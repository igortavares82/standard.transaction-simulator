using Microsoft.AspNetCore.Mvc;

namespace Standard.TransactionSimulator.WebApi.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Wellcome to transaction system";
        }
    }
}
