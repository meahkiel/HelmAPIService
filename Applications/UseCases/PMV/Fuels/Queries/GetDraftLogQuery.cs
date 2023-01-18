using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.Fuels.DTO;
using AutoMapper;

namespace Applications.UseCases.PMV.Fuels.Queries;

public record GetDraftLogQuery(string Fueler) : IRequest<Result<IEnumerable<FuelLogResponse>>>;

public class GetDraftLogQueryHandler : IRequestHandler<GetDraftLogQuery, Result<IEnumerable<FuelLogResponse>>>
{
    private readonly IUnitWork _unitWork;
    private readonly IMapper _mapper;
    private readonly ICommonService _commonService;

    public GetDraftLogQueryHandler(IUnitWork unitWork,
    ICommonService commonService,
    IMapper mapper)
    {
        _commonService = commonService;
        _mapper = mapper;
        _unitWork = unitWork;
    }
    
    public async Task<Result<IEnumerable<FuelLogResponse>>> Handle(GetDraftLogQuery request, CancellationToken cancellationToken)
    {
        try {
            
            var results = await _unitWork.FuelLogs.GetDraftLogs(request.Fueler);
            
            var response = results.Select(r => new FuelLogResponse {
                Id = r.Id.ToString(),   
                Station = r.StationCode,
                ReferenceNo = r.ReferenceNo,
                DocumentNo = r.DocumentNo,
                FueledDate = r.Date!.Value,
                ShiftStartTime = r.ShiftStartTime,
                ShiftEndTime = r.ShiftEndTime,
                OpeningMeter = r.OpeningMeter,
                Location = _commonService.GetLocationByKey("","id",r.LocationId).Result!.ProjectDepartment,
                Remarks = r.Remarks,
                Fueler = r.Fueler,
                IsPosted = r.Post.IsPosted,
                FuelTransactions = r.FuelTransactions.Select(t => new FuelDetailResponse {
                    Id = t.Id.ToString(),
                    AssetCode = t.AssetCode,
                    FuelDateTime = t.FuelDateTime,
                    LogType = t.LogType,
                    DriverQatarIdUrl = t.DriverQatarIdUrl ?? "",
                    OperatorDriver = t.Driver,
                    Quantity = t.Quantity,
                    Reading = t.Reading,
                    Remarks = t.Remarks ?? ""
                })
            });
            
            return Result.Ok(response!);
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
        
        
    }   
}
