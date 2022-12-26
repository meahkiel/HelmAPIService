using Applications.UseCases.PMV.LogSheets.DTO;
using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.LogSheets;

public record CreateLogSheetRequest(LogSheetOpenRequest LogSheetRequest) : IRequest<Result<LogSheetResponse>>;

public class LogSheetValidator : AbstractValidator<LogSheetOpenRequest> {
    
    
}

public class CreateLogSheetRequestHandler : IRequestHandler<CreateLogSheetRequest, Result<LogSheetResponse>>
{

    
    private readonly IUnitWork _unitWork;
    private readonly IValidator<LogSheetOpenRequest> _validator;

    public CreateLogSheetRequestHandler(IUnitWork unitWork,IValidator<LogSheetOpenRequest> validator)
    {
            _unitWork = unitWork;
            _validator = validator;
    }

     
    
    public async Task<Result<LogSheetResponse>> Handle(CreateLogSheetRequest request, CancellationToken cancellationToken)
    {

        try {
            
            var existingLog = await _unitWork.LogSheets.GetLatestRecord() ?? new LogSheet();

            //get the id of the station and location
            string locationId = "";
            string stationId = "";

            var logsheet = LogSheet.Create(existingLog.ReferenceNo,
                            DateTime.Parse(request.LogSheetRequest.ShiftStartTime),
                            request.LogSheetRequest.StartShiftTankerKm,
                            request.LogSheetRequest.StartShiftMeterReading,
                            locationId,stationId);
            
            
            _unitWork.LogSheets.Add(logsheet);

            await _unitWork.CommitSaveAsync(request.LogSheetRequest.EmployeeCode);

            return Result.Ok(new LogSheetResponse {
                    Id = logsheet.Id.ToString(),
                    ReferenceNo = logsheet.ReferenceNo,
                    ShiftStartTime = logsheet.ShiftStartTime.ToLongTimeString(),
                    StartShiftMeterReading = logsheet.StartShiftMeterReading,
                    StartShiftTankerKm = logsheet.StartShiftTankerKm,
                    FueledDate = logsheet.FueledDate,
                    Location = request.LogSheetRequest.Location,
                    Remarks = logsheet.Remarks
                });
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
        
        
    }
}