using Core.Common.ValueObjects;

namespace Core.PMV.LogSheets;

public class LogSheet : BaseEntity<Guid>
{

     public static LogSheet Create(
        int refNo,
        DateTime shiftStartTime,
        int startShiftTankerKm,
        int startShiftMeterReading,
        string locationId,
        string LVStationId)
    {

        return new LogSheet
        {
            ReferenceNo = refNo + 1,
            FueledDate = shiftStartTime,
            ShiftStartTime = shiftStartTime,
            StartShiftTankerKm = startShiftTankerKm,
            StartShiftMeterReading = startShiftMeterReading,
            LocationId = locationId,
            LVStationId = LVStationId
        };
    }

    public int ReferenceNo { get; set; }
    public DateTime FueledDate { get; set; } = DateTime.Now;
    public DateTime ShiftStartTime { get; set; }
    public DateTime? ShiftEndTime { get; set; }
    public int StartShiftTankerKm { get; set; }
    public int? EndShiftTankerKm { get; set; }
    public int StartShiftMeterReading { get; set; }
    public int? EndShiftMeterReading { get; set; }
    public string? Remarks { get; set; } = null;
    public string Fueler { get; set; } = "";

    public string LocationId { get; set; } = "";
    public string LVStationId { get; set; }

    private List<LogSheetDetail> _details;
    public IEnumerable<LogSheetDetail> Details => new List<LogSheetDetail>(_details);

    public PostingObject Post { get; set; } = new PostingObject();

    public void AddDetail(LogSheetDetail detail)
    {
        _details.Add(detail);
    }

     public void UpdateDetail(string id, string assetCode, int reading, float quantity, string? operatorDriver,
            string? driverQatariIdUrl, string? currentSMUUrl, string? tankMeterUrl)
    {

        var logSheetDetail = _details.FirstOrDefault(d => d.Id == Guid.Parse(id));

        if (logSheetDetail == null)
            throw new Exception("detail cannot be found");

        if (logSheetDetail.IsLessThanPrevious(reading)) {
            throw new Exception("Current reading must be higher than the previous");
        }

        logSheetDetail.AssetCode = assetCode;
        logSheetDetail.Reading = reading;
        logSheetDetail.Quantity = quantity;
        logSheetDetail.OperatorDriver = operatorDriver;

        if (!string.IsNullOrEmpty(driverQatariIdUrl))
            logSheetDetail.DriverQatarIdUrl = driverQatariIdUrl;

        if (!string.IsNullOrEmpty(currentSMUUrl))
            logSheetDetail.CurrentSMUUrl = currentSMUUrl;

        if (!string.IsNullOrEmpty(tankMeterUrl))
            logSheetDetail.TankMeterUrl = tankMeterUrl;
    }

    public void Posted()
    {
        Post = PostingObject.Posted();
    }

}