namespace Applications.UseCases.PMV.Fuels.Commands
{
    public record UnpostedCommand(string Id) : IRequest<Result<Unit>>;

    public class UnpostedCommandHandler : IRequestHandler<UnpostedCommand, Result<Unit>>
    {
        private readonly IUnitWork _unitWork;
        public UnpostedCommandHandler(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }

        public async Task<Result<Unit>> Handle(UnpostedCommand request, CancellationToken cancellationToken)
        {
            try {
                var log = await _unitWork.FuelLogs.GetLog(request.Id);
                if (log == null) throw new Exception("Fuel Log not found");
                log.TogglePost();
                
                return Result.Ok(Unit.Value);

            }
            catch(Exception ex) {
                return Result.Fail(ex.Message);
            }

        }
    }

}