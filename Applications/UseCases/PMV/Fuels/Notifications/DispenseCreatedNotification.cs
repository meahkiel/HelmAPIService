using Applications.UseCases.PMV.Common;
using Core.PMV.Assets;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Fuels.Notifications;

public record DispenseCreatedNotification(IEnumerable<FuelTransaction> Transactions) : INotification;

public class DispenseCreatedNotificationHandler : INotificationHandler<DispenseCreatedNotification>
{
    private readonly IUnitWork _unitWork;
    private readonly ICommonService _commonService;
    private readonly IUserAccessor _userAccessor;

    public DispenseCreatedNotificationHandler(IUnitWork unitWork,ICommonService commonService,IUserAccessor userAccessor)
    {
        _unitWork = unitWork;
        _commonService = commonService;
        _userAccessor = userAccessor;
    }

    public async Task Handle(
        DispenseCreatedNotification notification, CancellationToken cancellationToken)
    {
        
        //get dispense only 
        var dispenseTransactions = notification.Transactions
                                        .Where(p => p.LogType == EnumLogType.Dispense.ToString())
                                        .ToList();

        if(dispenseTransactions.Count() > 0 ) {

            var employeeCode = await _userAccessor.GetUserEmployeeCode();
            
            foreach(var transaction in dispenseTransactions) {

                var asset = await _commonService.GetAssetByCode(transaction.AssetCode);
                if(asset == null) continue;

                var assetRecord = await _unitWork.GetContext()
                                    .AssetRecord.Where(r => r.AssetId == asset.Id)
                                    .FirstOrDefaultAsync();
                
                if(assetRecord == null) {
                    
                    assetRecord = new AssetRecord {
                        AssetId =asset.Id,
                        LastServiceDate = transaction.FuelDateTime,
                        CurrentReading = transaction.Reading,
                        LatestTransactionId = transaction.Id.ToString(),
                        Source = "FuelLog"
                    };

                    _unitWork.Assets.AddAssetRecord(assetRecord);
                }
                else {

                    //check if the transaction is the same transaction
                    //otherwise check if the previous record is greater than the latest
                    //if not means the latest is not the actual latest reading reject it
                    if(assetRecord.LatestTransactionId != transaction.Id.ToString()) {
                        if(assetRecord.CurrentReading < transaction.Reading) continue;
                    }

                    assetRecord.CurrentReading = transaction.Reading;
                    assetRecord.LastServiceDate = transaction.FuelDateTime;
                    assetRecord.LatestTransactionId = transaction.Id.ToString();
                    _unitWork.Assets.UpdateAssetRecord(assetRecord);
                }
            }
        }
    }
}