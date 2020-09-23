using MediatR;

namespace Standard.TransactionSimulator.Applicator.Commands
{
    public class SimulateTransationCommand : IRequest
    {
        public int? QuantityOfEvents { get; set; }
    }
}
