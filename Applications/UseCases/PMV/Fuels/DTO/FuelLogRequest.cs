namespace Applications.UseCases.PMV.Fuels.DTO;

public record FuelLogOpenRequest(
    string FueledDate,
    string ShiftStartTime,
    int StartShiftTankerKm,
    int StartShiftMeterReading,
    string? Location,
    string Station,
    string? Remarks,
    string EmployeeCode);

public record FuelLogCloseRequest(
    string Id,
    string ShiftEndTime,
    int EndShiftTankerKm,
    int EndShiftMeterReading,
    string EmployeeCode,
    string? Remarks
);


public record FuelLogRequest {

    public string? Id { get; init; }
    public string EmployeeCode { get; init; }
    public string? FueledDate { get; init; }
    public int ReferenceNo { get; init; }
    public string ShiftStartTime { get; init; }
    public string? ShiftEndTime { get; init; }
    public int StartShiftTankerKm { get; init; }
    public int EndShiftTankerKm { get; init; }
    public string Location { get; init; }
    public string Station { get; init; }
    public string Remarks { get; init; }
    public string Fueler { get; init; }
    public IEnumerable<FuelTransactionsRequest> FuelTransactions { get; init; } = new List<FuelTransactionsRequest>();
    public bool IsPosted { get; init; }
    public MarkDelCollection? DelCollection { get; init; }
    public bool IsPartial { get; init; }
    
}

public class FuelTransactionsRequest
{
    public string? Id { get; set; }
    public string? FuelLogId { get; set; }
    public string AssetCode { get; set; } = "";
    public string FuelTime { get; set; } = "";
    public string? Remarks { get; set; }
    public string? OperatorDriver { get; set; } = "";
    public int Reading { get; set; } = 0;
    public float Quantity { get; set; } = 0f;
    public string? DriverQatarIdUrl { get; set; }
    public string LogType { get; set; }        
    public bool IsMarkDeleted { get; set; }

}

public class MarkDelCollection {
    public string[] Ids { get; set; }
}