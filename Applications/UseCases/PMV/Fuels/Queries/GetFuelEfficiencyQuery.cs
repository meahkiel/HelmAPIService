using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;

namespace Applications.UseCases.PMV.Fuels.Queries;

public record GetFuelEfficiencyQuery(string? AssetCode, DateTime DateFrom,DateTime DateTo,bool IsPostBack = false) : IRequest<Result<FuelLogReportResult>>;

public class GetFuelEfficiencyQueryHandler : IRequestHandler<GetFuelEfficiencyQuery, Result<FuelLogReportResult>>
{
    private readonly IFuelLogService _service;
    private readonly ICommonService _commonService;
    private readonly IUnitWork _unitwork;

    public GetFuelEfficiencyQueryHandler(IUnitWork unitwork,IFuelLogService service,ICommonService commonService)
    {
        _unitwork = unitwork;
        _service = service;
        _commonService = commonService;
    }
    public async Task<Result<FuelLogReportResult>> Handle(GetFuelEfficiencyQuery request, CancellationToken cancellationToken)
    {
        try {
            
            FuelLogReportResult result = new FuelLogReportResult();
            if(request.IsPostBack) {
                var effeciencyLists = await _service.GetFuelEffeciencyMasterList(request.DateFrom, request.DateTo,request.AssetCode);
                if(!string.IsNullOrEmpty(request.AssetCode)) {
                    string[] assets = Array.Empty<string>();
                    assets = request.AssetCode.Split(",");
                    result.EffeciencyLists = effeciencyLists.Where(e => assets.Contains(e.AssetCode)).ToList();
                }
                else {
                    result.EffeciencyLists = effeciencyLists;
                }
            }
            else {
                result.AssetSelections = await _commonService.GetInternalAssets();
            }

            return Result.Ok(result);
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }

    }
}