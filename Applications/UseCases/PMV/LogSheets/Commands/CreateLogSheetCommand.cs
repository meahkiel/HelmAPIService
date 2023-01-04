using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.LogSheets.DTO;
using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.LogSheets;

public record CreateLogSheetCommand(LogSheetOpenRequest LogSheetRequest) : IRequest<Result<LogSheetKeyResponse>>;

public class LogSheetValidator : AbstractValidator<LogSheetOpenRequest> {
    
    public LogSheetValidator()
    {   
        
        
    }
}

public class CreateLogSheetCommandHandler : IRequestHandler<CreateLogSheetCommand, Result<LogSheetKeyResponse>>
{

    
    private readonly IUnitWork _unitWork;
    private readonly IValidator<LogSheetOpenRequest> _validator;
    private readonly ICommonService _commonService;

    public CreateLogSheetCommandHandler(
        IUnitWork unitWork,
        IValidator<LogSheetOpenRequest> validator,
        ICommonService commonService)
    {
            _unitWork = unitWork;
            _validator = validator;
            _commonService = commonService;
    }

     
    
    public async Task<Result<LogSheetKeyResponse>> Handle(CreateLogSheetCommand request, CancellationToken cancellationToken)
    {

        try {
            
            var existingLog = await _unitWork.LogSheets.GetLatestRecord() ?? new LogSheet();

            //get the id of the station and location
            var location = await _commonService.GetLocationByKey(request.LogSheetRequest.Location);
            var locationId = (location == null) ?  0 : location.Id;

            var logsheet = LogSheet.Create(existingLog.ReferenceNo,
                            request.LogSheetRequest.ShiftStartTime,
                            request.LogSheetRequest.StartShiftTankerKm,
                            request.LogSheetRequest.StartShiftMeterReading,
                            locationId,request.LogSheetRequest.Station, 
                            request.LogSheetRequest.EmployeeCode);
                            
            _unitWork.LogSheets.Add(logsheet);
            await _unitWork.CommitSaveAsync(request.LogSheetRequest.EmployeeCode);
            
            return Result.Ok( new LogSheetKeyResponse {
                Id = logsheet.Id.ToString(),
                ReferenceNo = logsheet.ReferenceNo
            });
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
    }
}