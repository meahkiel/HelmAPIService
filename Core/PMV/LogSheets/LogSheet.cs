using Core.Common.ValueObjects;


namespace Core.PMV.LogSheets;

[AuditableAttribute]
public class LogSheet : BaseEntity<Guid>
{

     public static LogSheet Create(
        int refNo,
        string shiftStartTime,
        int startShiftTankerKm,
        int startShiftMeterReading,
        int locationId,
        string stationCode,
        string fueler)
    {

        return new LogSheet
        {
            Id = Guid.NewGuid(),
            ReferenceNo = refNo + 1,
            ShiftStartTime = DateTime.Parse(shiftStartTime),
            StartShiftTankerKm = startShiftTankerKm,
            StartShiftMeterReading = startShiftMeterReading,
            LocationId = locationId,
            StationCode = stationCode,
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

    public int LocationId { get; set; }
    
    public string StationCode { get; set; }


    private List<LogSheetDetail> _details = new List<LogSheetDetail>();
    public IEnumerable<LogSheetDetail> Details => new List<LogSheetDetail>(_details);

    public PostingObject Post { get; set; } = new PostingObject();

    public void AddDetail(LogSheetDetail detail)
    {   
        //implement domain business rules
        _details.Add(detail);
    }

    public void UpdateDetail(string detailId, string assetCode,int reading,float quantity,string operatorDriver,string transactionType) {
        
        var existingDetail = _details.Where(d => d.Id == Guid.Parse(detailId)).FirstOrDefault();
        if(!existingDetail.IsLessThanPrevious(reading)) {
            throw new Exception("Current Reading must not be less than the previous reading");
        }

        existingDetail.AssetCode = assetCode;
        existingDetail.Reading = reading;
        existingDetail.Quantity = quantity;
        existingDetail.OperatorDriver = operatorDriver;
        existingDetail.TransactionType = operatorDriver;
        existingDetail.Track = "update";
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