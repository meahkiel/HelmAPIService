using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Fuels.Queries;


public record GetFuelLogTransactionQuery(string? Stations, DateTime DateFrom, DateTime DateTo,bool IsPostBack = false) : IRequest<Result<FuelLogReportResult>>;

public class GetFuelLogTransactionQueryHandler : IRequestHandler<GetFuelLogTransactionQuery, Result<FuelLogReportResult>>
{
    private readonly IUnitWork _unitWork;
    private readonly IFuelLogService _service;
    private readonly ICommonService _commonService;

    public GetFuelLogTransactionQueryHandler(
        IUnitWork unitWork, 
        IFuelLogService service,
        ICommonService commonService)
    {
        _service = service;
        _commonService = commonService;
        _unitWork = unitWork;

    }
    public async Task<Result<FuelLogReportResult>> Handle(GetFuelLogTransactionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            FuelLogReportResult result = new FuelLogReportResult();
            //check if the request is new
            if(request.IsPostBack) {
                var masters = await FetchLogTransactions(request);
                if(!string.IsNullOrEmpty(request.Stations)) {
                    string[] stations = Array.Empty<string>();
                    stations = request.Stations.Split(",");
                    result.Masters = masters.Where(m => stations.Contains(m.Station)).ToList();
                }
                else {
                    result.Masters =masters;
                }
            }
            else {
                 result.StationSelections = await _commonService.GetAllStation();
            }


            return Result.Ok(result);

        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }


    }

    private async Task<IEnumerable<FuelLogMaster>> FetchLogTransactions(GetFuelLogTransactionQuery request)
    {
        var results = await _service.GetFuelLogMasterList(request.DateFrom, request.DateTo);
        
        foreach (var result in results) {
            result.TotalDispense = result.FuelTransactions
                                        .Where(t => t.LogType == EnumLogType.Dispense.ToString())
                                        .Sum(t => t.Quantity);
            result.TotalAdjustment = result.FuelTransactions
                                        .Where(t => t.LogType == EnumLogType.Adjustment.ToString())
                                        .Sum(t => t.Quantity);
            result.TotalRestock = result.FuelTransactions
                                        .Where(t => t.LogType == EnumLogType.Restock.ToString())
                                        .Sum(t => t.Quantity);
        }

        return results;
    }
}