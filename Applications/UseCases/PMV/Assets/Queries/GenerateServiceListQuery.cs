
using Applications.UseCases.PMV.Assets.DTO;

namespace Applications.UseCases.PMV.Assets.Queries;

public record GenerateServiceLogQuery(int Id) : IRequest<Result<IEnumerable<ServiceLogResponse>>>;



public class GenerateServiceLogQueryHandler : 
    IRequestHandler<GenerateServiceLogQuery, Result<IEnumerable<ServiceLogResponse>>>
{
        private readonly IUnitWork _unitWork;

    public GenerateServiceLogQueryHandler(IUnitWork unitWork)
    {
        _unitWork = unitWork;
    }
    public async Task<Result<IEnumerable<ServiceLogResponse>>> Handle(GenerateServiceLogQuery request, 
        CancellationToken cancellationToken)
    {
        try {
            //get the asset
            var asset = await _unitWork.Assets.ViewAssetById(request.Id);
            if(asset == null)
                throw new Exception("Asset Not Found");

            if(asset.ServiceLogs.Count() == 0) {
                var serviceAlert = await _unitWork.ServiceAlert.GetAlertServiceSetup(asset.Category);
                List<ServiceLogResponse> serviceLogs  = new List<ServiceLogResponse>();
                foreach(var detail in serviceAlert.Details) {
                    serviceLogs.Add(new ServiceLogResponse {
                        AssetId = request.Id,
                        ServiceCode = detail.ServiceCode,
                        Description = detail.Description,
                        KmAlert = detail.KmAlert,
                        KmInterval = detail.KmAlert
                    });
                }
            }    


        }
        catch(Exception ex) {

        }

    }
}
