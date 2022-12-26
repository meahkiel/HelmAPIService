using Applications.UseCases.PMV.LogSheets.DTO;
using Core.PMV.LogSheets;


namespace Applications.UseCases.PMV.LogSheets.Commands;


public record InsertUpdateLogDetailRequest(LogSheetDetailRequest DetailRequest) : IRequest<Result<LogSheetDetailResponse>>;


public class InsertUpdateLogDetailValidator : AbstractValidator<LogSheetDetailRequest> {

}

public class InsertUpdateLogDetailRequestHandler : IRequestHandler<InsertUpdateLogDetailRequest, Result<LogSheetDetailResponse>>
{
    private readonly IUnitWork _unitWork;
    private readonly IValidator<LogSheetDetailRequest> _validator;

    public InsertUpdateLogDetailRequestHandler(IUnitWork unitWork,IValidator<LogSheetDetailRequest> validator)
    {
        _unitWork = unitWork;
        _validator = validator;
    }
    public async Task<Result<LogSheetDetailResponse>> Handle(InsertUpdateLogDetailRequest request, CancellationToken cancellationToken)
    {
        try {
            
            LogSheetDetail? detail = new LogSheetDetail();
            if(String.IsNullOrEmpty(request.DetailRequest.Id)) {
                
                LogSheetDetail previousRecord = await GetPreviousRecord(request.DetailRequest);
                
                detail = LogSheetDetail.Create(
                                    assetCode: request.DetailRequest.AssetCode,
                                    fuelTime: DateTime.Parse(request.DetailRequest.FuelTime),
                                    reading: request.DetailRequest.Reading,
                                    previousReading: previousRecord!.Reading,
                                    quantity: request.DetailRequest.Quantity,
                                    driverQatarIdUrl: request.DetailRequest.DriverQatarIdUrl ?? "",
                                    currentSMUUrl: request.DetailRequest.CurrentSMUUrl ?? "",
                                    tankMeterUrl: request.DetailRequest.TankMeterUrl ?? "",
                                    operatorDriver: request.DetailRequest.OperatorDriver);
                
                _unitWork.LogSheets.AddDetail(detail);
            }
            else {
                //too much  consider rev.
                detail = await _unitWork.GetContext()
                                .LogSheetDetails
                                .Where(d => d.Id == Guid.Parse(request.DetailRequest.Id))
                                .FirstOrDefaultAsync();
                

                if(detail == null) throw new Exception("Log Sheet Detail no found");

                if(!detail.IsLessThanPrevious(request.DetailRequest.Reading)) {
                    throw new Exception("Current Reading must not be less than the previous reading");
                }

                detail.AssetCode = request.DetailRequest.AssetCode;
                detail.Reading = request.DetailRequest.Reading;
                detail.Quantity = request.DetailRequest.Quantity;
                detail.OperatorDriver = request.DetailRequest.OperatorDriver;
                
                _unitWork.LogSheets.UpdateDetail(detail);
            }

            await _unitWork.CommitSaveAsync(request.DetailRequest.EmployeeCode);

            return Result.Ok(new LogSheetDetailResponse {
                    Id = detail.Id.ToString(),
                    AssetCode = detail.AssetCode,
                    FuelTime = detail.FuelTime,
                    Quantity = detail.Quantity,
                    OperatorDriver = detail.OperatorDriver,
                    Reading = detail.Reading,
                    PreviousReading = detail.PreviousReading,
                    DriverQatarIdUrl = detail.DriverQatarIdUrl
                });
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
          
    }

    private async Task<LogSheetDetail> GetPreviousRecord(LogSheetDetailRequest request)
    {      
            //check the previous record
            var previousRecord = await _unitWork.GetContext()
                                    .LogSheetDetails.Where(d => d.AssetCode == request.AssetCode)
                                    .OrderByDescending(l => l.LogSheet.CreatedAt)
                                    .FirstOrDefaultAsync() ?? new LogSheetDetail();

            if (previousRecord.IsLessThan(request.Reading)) {
                throw new Exception("Error");
            }

            return previousRecord;
    }


}