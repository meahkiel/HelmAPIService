using Applications.UseCases.PMV.LogSheets.DTO;
using Applications.UseCases.PMV.Services;
using Core.PMV.LogSheets;


namespace Applications.UseCases.PMV.LogSheets.Commands;


public record InsertUpdateLogDetailRequest(LogSheetDetailRequest DetailRequest) : IRequest<Result<LogSheetDetailResponse>>;


public class InsertUpdateLogDetailValidator : AbstractValidator<LogSheetDetailRequest> {

}

public class InsertUpdateLogDetailRequestHandler : IRequestHandler<InsertUpdateLogDetailRequest, Result<LogSheetDetailResponse>>
{
    private readonly IUnitWork _unitWork;
    private readonly IValidator<LogSheetDetailRequest> _validator;
    private readonly ILogSheetValidationService _service;

    public InsertUpdateLogDetailRequestHandler(
        IUnitWork unitWork,
        IValidator<LogSheetDetailRequest> validator, 
        ILogSheetValidationService service)
    {
        _unitWork = unitWork;
        _validator = validator;
        _service = service;
    }
    public async Task<Result<LogSheetDetailResponse>> Handle(InsertUpdateLogDetailRequest request, CancellationToken cancellationToken)
    {
        try {
            
            
            LogSheetDetail? detail = new LogSheetDetail();
            var logSheet = await _unitWork.LogSheets.GetDraft(Guid.Parse(request.DetailRequest.LogSheetId));
            
            if(logSheet == null)  
                throw new Exception("Logsheet must not be null");

            if(String.IsNullOrEmpty(request.DetailRequest.Id)) {

                LogSheetDetail previousRecord = await _service
                                                        .GetLatestFuelLogRecord(request.DetailRequest.AssetCode,request.DetailRequest.Reading);
                
                detail = LogSheetDetail.Create(
                                    assetCode: request.DetailRequest.AssetCode,
                                    fuelTime: request.DetailRequest.FuelTime,
                                    reading: request.DetailRequest.Reading,
                                    previousReading: previousRecord!.Reading,
                                    quantity: request.DetailRequest.Quantity,
                                    driverQatarIdUrl: request.DetailRequest.DriverQatarIdUrl ?? "",
                                    currentSMUUrl: request.DetailRequest.CurrentSMUUrl ?? "",
                                    tankMeterUrl: request.DetailRequest.TankMeterUrl ?? "",
                                    operatorDriver: request.DetailRequest.OperatorDriver);
                
                detail.LogSheet = logSheet;    
                _unitWork.LogSheets.AddDetail(detail);
            }
            else {
                
                //too much  consider rev.
                detail = _unitWork.GetContext().LogSheetDetails.Where(d => d.Id == Guid.Parse(request.DetailRequest.Id)).FirstOrDefault();
                if(detail == null) throw new Exception("Log Sheet Detail no found");

                if(!detail.IsLessThanPrevious(request.DetailRequest.Reading)) {
                    throw new Exception("Current Reading must not be less than the previous reading");
                }

                detail.AssetCode = request.DetailRequest.AssetCode;
                detail.Reading = request.DetailRequest.Reading;
                detail.Quantity = request.DetailRequest.Quantity;
                detail.OperatorDriver = request.DetailRequest.OperatorDriver;
                
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

    

}