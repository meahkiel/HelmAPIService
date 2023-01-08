using Applications.UseCases.PMV.Fuels.DTO;
using Core.Utils;

namespace Applications.UseCases.PMV.Fuels.Queries
{
    public record GetTankTransactionQuery(string FuelStation, string DateFrom,string DateTo) : IRequest<Result<TankStoreReport>>;

    public class GetTankTransactionQueryHandler : IRequestHandler<GetTankTransactionQuery, Result<TankStoreReport>>
    {
        private readonly IUnitWork _unitWork;

        public GetTankTransactionQueryHandler(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public async Task<Result<TankStoreReport>> Handle(GetTankTransactionQuery request, CancellationToken cancellationToken)
        {
            try {
                var result = await _unitWork.FuelLogs.GetTankTransactions(
                    request.FuelStation,
                    request.DateFrom.ConvertToDateTime()!.Value,
                    request.DateTo.ConvertToDateTime()!.Value);
                    
                    TankStoreReport report = new TankStoreReport {
                        Summaries = result.Select(t => new TankStoreSummary {
                            StationCode = t.StationCode,
                            DocumentNo = t.DocumentNo,
                            OpeningMeter = t.OpeningMeter,
                            ClosingMeter = t.ClosingMeter,
                            TotalDispenseQty = t.TotalDispense,
                            TotalRestock = t.TotalRestock
                        })
                    };

                return Result.Ok(report);
            }
            catch(Exception ex) {
                return Result.Fail(ex.Message);
            }
        }
    }
}