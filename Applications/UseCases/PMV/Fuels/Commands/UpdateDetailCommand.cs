using Applications.UseCases.PMV.Fuels.DTO;

namespace Applications.UseCases.PMV.Fuels.Commands;

public record UpdateDetailCommand(FuelTransactionsRequest Request) : IRequest<Result<Unit>>;

public class UpdateDetailCommandHandler : IRequestHandler<UpdateDetailCommand, Result<Unit>>
{
    private readonly IUnitWork _unitWork;

    public UpdateDetailCommandHandler(IUnitWork unitWork)
    {
        _unitWork = unitWork;
    }
    public async Task<Result<Unit>> Handle(UpdateDetailCommand request, CancellationToken cancellationToken)
    {
        try {
            
            var transaction = await _unitWork.GetContext().FuelTransactions
                                    .Include(t => t.FuelLog)
                                    .SingleOrDefaultAsync(
                                        t => t.Id == Guid.Parse(request.Request.Id) && 
                                        t.FuelLog.Id == Guid.Parse(request.Request.FuelLogId));
            
            if(transaction == null)
                throw new Exception("Cannot find transaction");
            
            if(transaction.FuelLog.Post.IsPosted) 
                throw new Exception("Transaction is already posted");

            
            return Result.Ok(Unit.Value);

        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }

        
        


    }
}
