using Applications.UseCases.PMV.Fuels.DTO;

namespace Applications.UseCases.PMV.Fuels.Commands
{
    public record UpdateDetailCommand(FuelTransactionsRequest Request) : IRequest<Result<Unit>>;

    public class UpdateDetailCommandHandler : IRequestHandler<UpdateDetailCommand, Result<Unit>>
    {
        private readonly IUnitWork _unitWork;

        public UpdateDetailCommandHandler(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public Task<Result<Unit>> Handle(UpdateDetailCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
