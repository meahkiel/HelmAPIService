using BaseEntityPack.Core;
using Core.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.PMV.Fuels;


public enum EnumLogType { Dispense, Refill,Adjustment }

[AuditableAttribute]
public class FuelTransaction : Entity<Guid>
{
    public FuelTransaction() : base(Guid.NewGuid())
    {
        
    }
    public FuelTransaction(
           Guid id,
        string assetCode,
        string fuelStation,
        string fuelDate,
        string fuelTime,
        float qty
    ) : base(id)
    {
        var fuelDateTime = fuelDate.MergeAndConvert(
                                    fuelTime.ConvertToDateTime()!.Value.ToLongTimeString());
        AssetCode = assetCode;
        FuelStation = fuelStation;
        FuelDateTime = fuelDateTime;
        Quantity = qty;
        LogType = EnumLogType.Refill.ToString();
        SourceType = "Tank";
    }
    public FuelTransaction(
        Guid id,
        string assetCode,
        int previousReading,
        int reading,
        string driver,
        string fuelStation,
        string fuelDate,
        string fuelTime,
        float qty,
        string logType,
        string sourceType = "Equipment") : base(id)
    {
        var fuelDateTime = fuelDate.MergeAndConvert(
                                    fuelTime.ConvertToDateTime()!.Value.ToLongTimeString());
            
        AssetCode = assetCode;
        PreviousReading = previousReading;
        Reading = reading;
        Driver = driver;
        FuelStation = fuelStation;
        FuelDateTime = fuelDateTime;
        Quantity = qty;
        LogType = logType;
        SourceType = sourceType;

    }
    
    public static float ToLiterPerHour(float previousReading,float reading,float quantity) => previousReading == 0 ? 0 : quantity / (previousReading - reading);
    public static float ToHourPerLiter(float previousReading,float reading,float quantity) => previousReading == 0 ? 0 : (previousReading - reading) / quantity;
    
    public float GetActualQuantity() => LogType == EnumLogType.Dispense.ToString() ?
            Quantity * -1 : Quantity;

    public float GetDispenseQuantity() => LogType == EnumLogType.Dispense.ToString() ? Quantity : 0f;
    
    public string? FuelStation { get; set; }
    public DateTime FuelDateTime { get; set; } = DateTime.Today;
    public float Quantity { get; set; }
    public string SourceType { get; set; }
    public string AssetCode { get; set; }
    public int PreviousReading { get; set; }
    public int Reading { get; set; }
    public string? Driver { get; set; }
    public string? Remarks { get; set; }
    public string LogType { get; set; }
    public string? DriverQatarIdUrl { get; set; }

    public FuelLog FuelLog { get; set; }

    [Timestamp]
    public DateTime RowVersion { get; set; }

    [NotMapped]
    public string Track { get; set; }
    public bool IsLessThan(int currentReading) => Reading <= currentReading;
    public bool IsLessThanPrevious(int currentReading) => PreviousReading <= currentReading;

}