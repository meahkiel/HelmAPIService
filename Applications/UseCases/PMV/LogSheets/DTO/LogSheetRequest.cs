
public record LogSheetOpenRequest(
    string FueledDate,
    string ShiftStartTime,
    int StartShiftTankerKm,
    int StartShiftMeterReading,
    string Location,
    string LVStation,
    string? Remarks,
    string EmployeeCode);

public record LogSheetCloseRequest(
    string Id,
    string ShiftEndTime,
    int EndShiftTankerKm,
    int EndShiftMeterReading,
    string EmployeeCode,
    string? Remarks
);

public record LogSheetRequest(
    string? Id,
    string ShiftEndTime,
    int EndShiftTankerKm,
    int EndShiftMeterReading,
    string EmployeeCode,
    string FueledDate,
    string ShiftStartTime,
    int StartShiftTankerKm,
    int StartShiftMeterReading,
    string Location,
    string LVStation,
    string? Remarks,
    string Fueler,
    ICollection<LogSheetDetailRequest>? details
);



public class LogSheetDetailRequest
{

    public string? Id { get; set; }

    public string LogSheetId { get; set; } = null!;

    public string AssetCode { get; set; } = "";
    public string FuelTime { get; set; } = "";
    public string? OperatorDriver { get; set; } = "";
    public int Reading { get; set; } = 0;
    public float Quantity { get; set; } = 0f;

    public string? DriverQatarIdUrl { get; set; }
    public string? CurrentSMUUrl { get; set; }
    public string? TankMeterUrl { get; set; }

    public string? UIState { get; set; }
}