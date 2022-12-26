using Core.Common.ValueObjects;

namespace Core.PMV.LogSheets;

public class LogSheet : BaseEntity<Guid>
{

     public static LogSheet Create(
        int refNo,
        string shiftStartTime,
        int startShiftTankerKm,
        int startShiftMeterReading,
        string locationId,
        string LVStationId,
        string fueler)
    {

        return new LogSheet
        {
            ReferenceNo = refNo + 1,
            ShiftStartTime = DateTime.Parse(shiftStartTime),
            StartShiftTankerKm = startShiftTankerKm,
            StartShiftMeterReading = startShiftMeterReading,
            LocationId = locationId,
            LVStationId = LVStationId,
            Fueler = fueler
        };
    }

    public int ReferenceNo { get; set; }
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

    public void CloseLogSheet(int endShiftMeterReading, int endShiftTankerKm, string shiftEndTime,string? remarks) {
        EndShiftMeterReading = endShiftMeterReading;
        EndShiftTankerKm = endShiftTankerKm;
        ShiftEndTime =  DateTime.Parse(shiftEndTime);
        Remarks = remarks ?? "";
        this.Posted();
    }

    

    public void Posted()
    {
        Post = PostingObject.Posted();
    }

}