using Applications.UseCases.PMV.ServiceAlerts.DTO;

namespace Applications.UseCases.PMV.ServiceAlerts.Queries;

public record GetAlertByIdRequest(string Id) : IRequest<Result<ServiceAlertResponse>>;

public class GetAlertByIdRequestHandler : IRequestHandler<GetAlertByIdRequest, Result<ServiceAlertResponse>> {
    
    private readonly IUnitWork _unitWork;

    public GetAlertByIdRequestHandler(IUnitWork unitWork)
    {
            _unitWork = unitWork;
    }


    public async Task<Result<ServiceAlertResponse>> Handle(GetAlertByIdRequest request, CancellationToken cancellationToken) {
        
        try {

            var alert = await _unitWork.ServiceAlert.GetByIdAsync(Guid.Parse(request.Id));
            
            if(alert == null) {
                throw new Exception("Alert not found");
            }

            return Result.Ok(new ServiceAlertResponse {
                Id = alert.Id.ToString(),
                GroupName = alert.GroupName,
                Description = alert.Description,
                Details = alert.Details.Select(
                    p => new ServiceAlertDetailResponse {
                        Id = p.Id.ToString(),
                        ServiceCode = p.ServiceCode,
                        KmAlert = p.KmAlert,
                        KmInterval = p.KmInterval
                    })
            });
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
    }
}
