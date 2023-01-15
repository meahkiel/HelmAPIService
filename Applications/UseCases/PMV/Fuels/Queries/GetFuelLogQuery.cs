using Applications.UseCases.Common;
using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.Fuels.DTO;
using AutoMapper;

namespace Applications.UseCases.PMV.Fuels.Queries;


/// <summary>
/// Set an initial value whenever the form is created
/// prepare the lookup value 
///     station
///     
/// </summary>

public record GetFuelLogQuery(string? Id,bool IsPostBack = false) : IRequest<Result<FuelLogResponse>>;

public class GetFuelLogQueryHandler : IRequestHandler<GetFuelLogQuery, Result<FuelLogResponse>>
{
    private readonly ICommonService _commonService;
    private readonly IUnitWork _unitWork;
    private readonly IMapper _mapper;

    public GetFuelLogQueryHandler(ICommonService commonService,IUnitWork unitWork,IMapper mapper)
	{
        _commonService = commonService;
        _unitWork = unitWork;
        _mapper = mapper;
    }
    
    public async Task<Result<FuelLogResponse>> Handle(GetFuelLogQuery request, CancellationToken cancellationToken)
    {
        try
        {
            FuelLogResponse response = new FuelLogResponse();
            
            if(!string.IsNullOrEmpty(request.Id)) {
                var log = await _unitWork.FuelLogs.GetLog(request.Id);
                if(log == null)
                    throw new Exception("Fuel Log not found");
                
                response = _mapper.Map<FuelLogResponse>(log);
                var location = await _commonService.GetLocationByKey("","id",log.LocationId);
                response.Location = location.ProjectDepartment;
            }

             if(!request.IsPostBack) {
                var stations = await _commonService.GetAllStation();
                
                response.StationSelections = await _commonService.GetAllStation();
                response.LocationSelections = (await _commonService.GetLocations())
                                                .Select(s => new SelectItem {Value = s.ProjectDepartment,Text = s.ProjectDepartment});
                response.AssetCodeSelections = await _commonService.GetAssetLookup();
                response.FuelerSelections = await _commonService.GetEmployees();
            }
            
            return Result.Ok(response);
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }


    }
}
