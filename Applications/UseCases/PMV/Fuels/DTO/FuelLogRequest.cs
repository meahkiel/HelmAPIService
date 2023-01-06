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

public record FuelLogRequest(
    string? Id,
    string? EmployeeCode,
    string FueledDate,
    string ShiftStartTime,
    string ShiftEndTime,
    float StartShiftTankerKm,
    float EndShiftTankerKm,
    float StartShiftMeterReading,
    float EndShiftMeterReading,
    string Location,
    string Station,
    string? Remarks,
    string Fueler,
    bool IsPosted,
    ICollection<LogSheetDetailRequest>? details

);

public class LogSheetDetailRequest
{
    public string? Id { get; set; }
    public string? LogSheetId { get; set; }
    public string? FuelStation { get; set; }
    public string AssetCode { get; set; } = "";
    public string FuelDate { get; set; }
    public string FuelTime { get; set; } = "";
    public string? OperatorDriver { get; set; } = "";
    public int Reading { get; set; } = 0;
    public float Quantity { get; set; } = 0f;
    public string? DriverQatarIdUrl { get; set; }
    public string? EmployeeCode { get; set; }
    public string LogType { get; set; }
        
    public bool IsMarkDeleted { get; set; }

}