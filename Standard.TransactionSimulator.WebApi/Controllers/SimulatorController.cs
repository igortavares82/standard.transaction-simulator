using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.TransactionSimulator.Applicator.Commands;
using System.Threading.Tasks;

namespace Standard.TransactionSimulator.WebApi.Controllers
{
    [ApiController]
    [Route("api/simulator")]
    public class SimulatorController : Controller
    {
        private IMediator Mediator { get; }

        public SimulatorController(IMediator mediator) 
        {
            Mediator = mediator;
        }

        [HttpPost]
        public async Task Post([FromBody]SimulateTransationCommand command) => await Mediator.Send(command);
    }
}
 