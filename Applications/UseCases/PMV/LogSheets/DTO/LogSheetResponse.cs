namespace Applications.UseCases.PMV.LogSheets.DTO;


public class LogSheetKeyResponse {
    public string Id { get; set; }
    public int ReferenceNo { get; set; }
}

public class LogSheetDetailKeyResponse {
    public string Id { get; set; }
    public string LogSheetId { get; set; }
}


public class LogSheetResponse
{

    public string? Id { get; set; } = null;

    public int ReferenceNo { get; set; }

    public DateTime FueledDate { get; set; }

    public string? ShiftStartTime { get; set; }

    public string? ShiftEndTime { get; set; }

    public int StartShiftTankerKm { get; set; }

    public int? EndShiftTankerKm { get; set; }

    public int StartShiftMeterReading { get; set; }

    public int? EndShiftMeterReading { get; set; }

    public string Location { get; set; } = "";

    public string Station { get; set; } = "";

    public string? Remarks { get; set; } = null;

    public string Fueler { get; set; } = null!;

    public IEnumerable<LogSheetDetailResponse> Details { get; set; } = new List<LogSheetDetailResponse>();
}


public class LogSheetDetailResponse {

    public string LogSheetId { get; set; }
    public string Id { get; set; }
    public string AssetCode { get; set; } = "";
    public DateTime FuelTime { get; set; } = DateTime.Now;
    public string? OperatorDriver { get; set; } = "";
    public int Reading { get; set; } = 0;
    //readonly
    public int PreviousReading { get; set; } 
    public float Quantity { get; set; } = 0f;
    public string DriverQatarIdUrl { get; set; }
    public string TransactionType { get; set; }

    
}