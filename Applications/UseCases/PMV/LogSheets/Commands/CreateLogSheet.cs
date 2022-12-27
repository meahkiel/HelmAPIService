using Applications.UseCases.PMV.Common;
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
    private readonly ICommonService _commonService;

    public CreateLogSheetRequestHandler(
        IUnitWork unitWork,
        IValidator<LogSheetOpenRequest> validator,
        ICommonService commonService)
    {
            _unitWork = unitWork;
            _validator = validator;
            _commonService = commonService;
    }

     
    
    public async Task<Result<LogSheetResponse>> Handle(CreateLogSheetRequest request, CancellationToken cancellationToken)
    {

        try {
            
            var existingLog = await _unitWork.LogSheets.GetLatestRecord() ?? new LogSheet();

            //get the id of the station and location
            var location = await _commonService.GetLocationByKey(request.LogSheetRequest.Location);
            
            var logsheet = LogSheet.Create(existingLog.ReferenceNo,
                            request.LogSheetRequest.ShiftStartTime,
                            request.LogSheetRequest.StartShiftTankerKm,
                            request.LogSheetRequest.StartShiftMeterReading,
                            location.Id,request.LogSheetRequest.LVStation, 
                            request.LogSheetRequest.EmployeeCode);
                            
            _unitWork.LogSheets.Add(logsheet);

            await _unitWork.CommitSaveAsync(request.LogSheetRequest.EmployeeCode);

            return Result.Ok(new LogSheetResponse {
                    Id = logsheet.Id.ToString(),
                    ReferenceNo = logsheet.ReferenceNo,
                    ShiftStartTime = logsheet.ShiftStartTime.ToLongTimeString(),
                    StartShiftMeterReading = logsheet.StartShiftMeterReading,
                    StartShiftTankerKm = logsheet.StartShiftTankerKm,
                    FueledDate = logsheet.ShiftStartTime,
                    Location = request.LogSheetRequest.Location,
                    Remarks = logsheet.Remarks
                });
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
        
        
    }
}