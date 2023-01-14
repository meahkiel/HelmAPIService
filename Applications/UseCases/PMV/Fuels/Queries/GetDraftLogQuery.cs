using Applications.UseCases.PMV.Fuels.DTO;
using AutoMapper;

namespace Applications.UseCases.PMV.Fuels.Queries;

public record GetDraftLogQuery(string Station,string Fueler) : IRequest<Result<IEnumerable<FuelLogResponse>>>;

public class GetDraftLogQueryHandler : IRequestHandler<GetDraftLogQuery, Result<IEnumerable<FuelLogResponse>>>
{
    private readonly IUnitWork _unitWork;
    private readonly IMapper _mapper;

    public GetDraftLogQueryHandler(IUnitWork unitWork,IMapper mapper)
    {
        _mapper = mapper;
        _unitWork = unitWork;
    }
    
    public async Task<Result<IEnumerable<FuelLogResponse>>> Handle(GetDraftLogQuery request, CancellationToken cancellationToken)
    {
        try {
            
            var result = await _unitWork.FuelLogs.GetDraftLogs(request.Station,request.Fueler);
            
            return Result.Ok(_mapper.Map<IEnumerable<FuelLogResponse>>(result));
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
        
        
    }   
}
