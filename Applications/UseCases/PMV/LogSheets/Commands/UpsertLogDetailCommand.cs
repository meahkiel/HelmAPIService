using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.LogSheets.DTO;
using Core.PMV.LogSheets;


namespace Applications.UseCases.PMV.LogSheets.Commands;


public record UpsertLogDetailCommand(LogSheetDetailRequest DetailRequest) : IRequest<Result<LogSheetDetailResponse>>;


public class UpsertLogDetailValidator : AbstractValidator<LogSheetDetailRequest> {

}

public class UpsertLogDetailCommandHandler : IRequestHandler<UpsertLogDetailCommand, Result<LogSheetDetailResponse>>
{
    private readonly IUnitWork _unitWork;
    private readonly IValidator<LogSheetDetailRequest> _validator;
    private readonly ICommonService _service;

    public UpsertLogDetailCommandHandler(
        IUnitWork unitWork,
        IValidator<LogSheetDetailRequest> validator, 
        ICommonService service)
    {
        _unitWork = unitWork;
        _validator = validator;
        _service = service;
    }

   private async Task<LogSheetDetail> GetLatestFuelLogRecord(string assetCode,int currentReading)
        {
            var previousRecord = await _unitWork.GetContext()
                                    .LogSheetDetails.Where(d => d.AssetCode == assetCode)
                                    .OrderByDescending(l => l.Quantity)
                                    .OrderByDescending(l => l.FuelTime)
                                    .FirstOrDefaultAsync() ?? new LogSheetDetail();

            if(!previousRecord.IsLessThan(currentReading)) 
                throw new Exception("Current reading must be higher than the previous SMU");

            return previousRecord;
    }



    public async Task<Result<LogSheetDetailResponse>> Handle(UpsertLogDetailCommand request, CancellationToken cancellationToken)
    {
        try {

            LogSheetDetail? detail = new LogSheetDetail();
            var logSheet = await _unitWork.LogSheets.GetDraft(Guid.Parse(request.DetailRequest.LogSheetId));
            
            if(logSheet == null)  
                throw new Exception("Logsheet must not be null");

            if(String.IsNullOrEmpty(request.DetailRequest.Id)) {
                
                //upset tank check if the station is main
                var station = await _service.GetStationByCode(logSheet.StationCode);
                var reading = 0;
                
                if(station!.StationType != "main") {
                    //post condition
                    LogSheetDetail previousRecord = await GetLatestFuelLogRecord(request.DetailRequest.AssetCode,request.DetailRequest.Reading);
                    reading = previousRecord.Reading;
                }

                
                detail = LogSheetDetail.Create(
                                    assetCode: request.DetailRequest.AssetCode,
                                    fuelTime: request.DetailRequest.FuelTime,
                                    reading: request.DetailRequest.Reading,
                                    previousReading: reading,
                                    quantity: request.DetailRequest.Quantity,
                                    driverQatarIdUrl: request.DetailRequest.DriverQatarIdUrl ?? "",
                                    transactionType: request.DetailRequest.TransactionType,
                                    operatorDriver: request.DetailRequest.OperatorDriver);
                
                logSheet.AddDetail(detail);
            }
            else {
                logSheet.UpdateDetail(
                    request.DetailRequest.Id,
                    request.DetailRequest.AssetCode,
                    request.DetailRequest.Reading,
                    request.DetailRequest.Quantity,
                    request.DetailRequest.OperatorDriver,
                    request.DetailRequest.TransactionType);
            }

            await _unitWork.CommitSaveAsync(request.DetailRequest.EmployeeCode);
            return Result.Ok(new LogSheetDetailResponse {Id = detail.Id.ToString()});
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
          
    }

    

}