using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;

namespace Applications.UseCases.PMV.Fuels.Queries;

public record GetFuelEfficiencyQuery(string? AssetCodes, DateTime DateFrom,DateTime DateTo,bool IsPostBack = false) : IRequest<Result<FuelLogReportResult>>;

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
                result.EffeciencyLists = await _service.GetFuelEffeciencyMasterList(request.DateFrom, request.DateTo,request.AssetCodes);
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