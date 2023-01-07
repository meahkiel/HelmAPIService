using Applications.UseCases.PMV.LogSheets.DTO;
using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.LogSheets.Queries;

public record GetLogSheetByIdRequest(string Id) : IRequest<Result<LogSheetResponse>>;

public class GetLogSheetsByIdRequestHandler : IRequestHandler<GetLogSheetByIdRequest, Result<LogSheetResponse>>
{
        private readonly IUnitWork _unitWork;

    public GetLogSheetsByIdRequestHandler(IUnitWork unitWork)
    {
            _unitWork = unitWork;
    }


    public async Task<Result<LogSheetResponse>> Handle(GetLogSheetByIdRequest request, CancellationToken cancellationToken)
    {
        try {
            
            var logsheet = await _unitWork.LogSheets.GetByIdAsync(Guid.Parse(request.Id));

            if(logsheet == null) 
                throw new Exception("Cannot find Logsheet");

            return Result.Ok(new LogSheetResponse 
            {
                Id = logsheet.Id.ToString(),
                ReferenceNo = logsheet.ReferenceNo,
                ShiftStartTime = logsheet.ShiftStartTime.ToLongTimeString(),
                ShiftEndTime = logsheet.ShiftEndTime.HasValue ? logsheet.ShiftEndTime.Value.ToLongTimeString() : null,
                StartShiftMeterReading = logsheet.StartShiftMeterReading,
                EndShiftMeterReading = logsheet.EndShiftMeterReading ?? 0,
                StartShiftTankerKm = logsheet.StartShiftTankerKm,
                EndShiftTankerKm = logsheet.EndShiftTankerKm ?? 0,
                FueledDate = logsheet.ShiftStartTime,
                Fueler = logsheet.UpdatedBy!,
                Location = "",//logsheet.LocationId,
                Remarks = logsheet.Remarks,
                Station = logsheet.StationCode,
                Details = ExtractDetail(logsheet.Details).ToList()
            });
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
    }


    private IEnumerable<LogSheetDetailResponse> ExtractDetail(IEnumerable<LogSheetDetail> details) {
                var responses = new List<LogSheetDetailResponse>();
                
                foreach(var detail in details) {
                    responses.Add(new LogSheetDetailResponse {
                        Id = detail.Id.ToString(),
                        LogSheetId = detail.LogSheet.Id.ToString(),
                        AssetCode = detail.AssetCode,
                        FuelTime = detail.FuelTime,
                        OperatorDriver = detail.OperatorDriver,
                        Reading = detail.Reading,
                        PreviousReading = detail.PreviousReading,
                        Quantity = detail.Quantity,
                        DriverQatarIdUrl = detail.DriverQatarIdUrl,
                    });
                }

                return responses;
            }
}
