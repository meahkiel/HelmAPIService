using Applications.UseCases.PMV.ServiceAlerts.DTO;

namespace Applications.UseCases.PMV.ServiceAlerts.Queries;

public record GetAlertsRequest() : IRequest<Result<IEnumerable<ServiceAlertResponse>>>;

public class GetAlertsRequestHandler : IRequestHandler<GetAlertsRequest, Result<IEnumerable<ServiceAlertResponse>>>
{

    private readonly IUnitWork _unitWork;

    public GetAlertsRequestHandler(IUnitWork unitWork)
    {
        _unitWork = unitWork;
    }
    public async Task<Result<IEnumerable<ServiceAlertResponse>>> Handle(GetAlertsRequest request, CancellationToken cancellationToken)
    {
        try {
            
            var results = await _unitWork.ServiceAlert.GetAll();

            return Result.Ok(results.Select(r => new ServiceAlertResponse 
                    {Id = r.Id.ToString(), GroupName = r.GroupName,Description = r.Description}));

        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
    }
}
