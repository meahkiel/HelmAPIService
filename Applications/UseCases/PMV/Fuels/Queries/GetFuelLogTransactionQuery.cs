using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Fuels.Queries;


public record GetFuelLogTransactionQuery(string? Stations, DateTime DateFrom, DateTime DateTo) : IRequest<Result<IEnumerable<FuelLogMaster>>>;

public class GetFuelLogTransactionQueryHandler : IRequestHandler<GetFuelLogTransactionQuery, Result<IEnumerable<FuelLogMaster>>>
{
    private readonly IUnitWork _unitWork;
    private readonly IFuelLogService _service;

    public GetFuelLogTransactionQueryHandler(IUnitWork unitWork, IFuelLogService service)
    {
        _service = service;
        _unitWork = unitWork;

    }
    public async Task<Result<IEnumerable<FuelLogMaster>>> Handle(GetFuelLogTransactionQuery request, CancellationToken cancellationToken)
    {

        try
        {

            string[] stations = Array.Empty<string>();
            //paramter
            if (!string.IsNullOrEmpty(request.Stations))
            {
                stations = request.Stations.Split(",");
            }

            var results = await _service.GetFuelLogMasterList(request.DateFrom,request.DateTo);


            foreach(var result in results) {
                
                result.TotalDispense = result.FuelTransactions
                                            .Where(t => t.LogType == EnumLogType.Dispense.ToString())
                                            .Sum(t => t.Quantity);

                result.TotalAdjustment = result.FuelTransactions
                                            .Where(t => t.LogType == EnumLogType.Adjustment.ToString())
                                            .Sum(t => t.Quantity);
                
                result.TotalRestock = result.FuelTransactions
                                             .Where(t => t.LogType == EnumLogType.Restock.ToString())
                                             .Sum(t => t.Quantity);

                // if(result.StationType.ToLower() != "main") {
                //     var totalRestock = results.Where(r => r.FuelTransactions.Any(t => t.AssetCode == result.Station))
                //                                     .Sum(t => t.FuelTransactions.Sum(f => f.Quantity));
                //     result.TotalRestock = totalRestock;
                // }
                // else {
                //     
                // }
            }

            if (stations.Count() > 0)
            {
                
            }

            return Result.Ok(results);

        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }


    }
}