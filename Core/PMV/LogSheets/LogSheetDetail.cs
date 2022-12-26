namespace Core.PMV.LogSheets;

public class LogSheetDetail : BaseEntity<Guid>
{
    public string AssetCode { get; set; } = "";
    public DateTime FuelTime { get; set; } = DateTime.Now;
    public string? OperatorDriver { get; set; } = "";
    public int Reading { get; set; } = 0;
    //readonly
    public int PreviousReading { get; private set; }
    public float Quantity { get; set; } = 0f;
    public string DriverQatarIdUrl { get; set; }
    public string CurrentSMUUrl { get; set; }
    public string TankMeterUrl { get; set; }
    public LogSheet LogSheet { get; set; }

    public bool IsLessThan(int currentReading) => Reading < currentReading;

    public bool IsLessThanPrevious(int currentReading) => PreviousReading > currentReading;

    public static LogSheetDetail CreateNoAutoId(
        string id,
        LogSheet logSheet,
        string assetCode,
        DateTime fuelTime,
        int reading,
        int previousReading,
        float quantity,
        string driverQatarIdUrl,
        string currentSMUUrl,
        string tankMeterUrl,
        string? operatorDriver
    )
    {
        return new LogSheetDetail
        {
            Id = Guid.Parse(id),

            AssetCode = assetCode,
            FuelTime = fuelTime,
            Reading = reading,
            PreviousReading = previousReading,
            Quantity = quantity,
            DriverQatarIdUrl = driverQatarIdUrl,
            CurrentSMUUrl = currentSMUUrl,
            TankMeterUrl = tankMeterUrl,
            OperatorDriver = operatorDriver,
            LogSheet = logSheet
        };
    }
    public static LogSheetDetail Create(
        string assetCode,
        DateTime fuelTime,
        int reading,
        int previousReading,
        float quantity,
        string driverQatarIdUrl,
        string currentSMUUrl,
        string tankMeterUrl,
        string? operatorDriver)
    {

        return new LogSheetDetail
        {
            AssetCode = assetCode,
            FuelTime = fuelTime,
            Reading = reading,
            PreviousReading = previousReading,
            Quantity = quantity,
            DriverQatarIdUrl = driverQatarIdUrl,
            CurrentSMUUrl = currentSMUUrl,
            TankMeterUrl = tankMeterUrl,
            OperatorDriver = operatorDriver
        };
    }
}