using Applications.UseCases.PMV.Assets.DTO;
using AutoMapper;
using Core.PMV.Assets;

namespace Applications.UseCases.PMV.Assets.Events;

public record AssetUpsertEvent(AssetViewResponse AssetView) : INotification;

public class AssetUpsertEventHandler : INotificationHandler<AssetUpsertEvent>
{
    private readonly IUnitWork _unitWork;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public AssetUpsertEventHandler(IUnitWork unitWork,IMapper mapper,IUserAccessor userAccessor)
    {
        _unitWork = unitWork;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task Handle(AssetUpsertEvent notification, CancellationToken cancellationToken)
    {
        AssetRecord record = new AssetRecord();
        if(!_unitWork.GetContext().AssetRecord.Any(a => a.AssetId == notification.AssetView.Id)) {
            record = CreateAssetRecord(notification.AssetView);
            _unitWork.Assets.AddAssetRecord(record);
        }
        var serviceLogs = await CreateServiceLogs(notification.AssetView);
        if(serviceLogs.Count() > 0) {
            foreach(var serviceLog in serviceLogs) {
                _unitWork.Assets.AddServiceLog(serviceLog);
            }
            
        }

        var employeeCode = await _userAccessor.GetUserEmployeeCode();
        await _unitWork.CommitSaveAsync(employeeCode);
    }

    private async Task<IEnumerable<ServiceLog>> CreateServiceLogs(AssetViewResponse assetView) {
        
        List<ServiceLog> serviceLogs  = new List<ServiceLog>();


        if(assetView.ServiceLogs.Count() == 0) {
            
            var serviceAlert = await _unitWork.ServiceAlert.GetAlertServiceSetup(assetView.Category);
            foreach(var detail in serviceAlert.Details) {
                serviceLogs.Add(new ServiceLog {
                    AssetId = assetView.Id,
                    LastReading = assetView.KmHr,
                    ServiceCode = detail.ServiceCode,
                    KmAlert = detail.KmAlert,
                    KmInterval = detail.KmAlert
                });
            }
        }

        return serviceLogs;
    }

    private AssetRecord CreateAssetRecord(AssetViewResponse assetView) => new AssetRecord
    {
        AssetId = assetView.Id,
        CurrentReading = assetView.KmHr
    };
}
