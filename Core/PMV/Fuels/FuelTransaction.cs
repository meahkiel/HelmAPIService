using BaseEntityPack.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.PMV.Fuels;


public enum EnumLogType { Dispense, Restock }

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
        DateTime fuelDateTime,
        float qty
    ) : base(id)
    {
        AssetCode = assetCode;
        FuelStation = fuelStation;
        FuelDateTime = fuelDateTime;
        Quantity = qty;
        LogType = EnumLogType.Restock.ToString();
    }
    public FuelTransaction(
        Guid id,
        string assetCode,
        int previousReading,
        int reading,
        string driver,
        string fuelStation,
        DateTime fuelDateTime,
        float qty) : base(id)
    {

        AssetCode = assetCode;
        PreviousReading = previousReading;
        Reading = reading;
        Driver = driver;
        FuelStation = fuelStation;
        FuelDateTime = fuelDateTime;
        Quantity = qty;
        LogType = EnumLogType.Dispense.ToString();

    }


    public float GetActualQuantity() => LogType == EnumLogType.Dispense.ToString() ?
            Quantity * -1 : Quantity;

    public float GetDispenseQuantity() => LogType == EnumLogType.Dispense.ToString() ? Quantity : 0f;
    
    public string? FuelStation { get; set; }
    public DateTime FuelDateTime { get; set; }
    public float Quantity { get; set; }
    public string AssetCode { get; set; }
    public int PreviousReading { get; set; }
    public int Reading { get; set; }
    public string? Driver { get; set; }
    public string? Remarks { get; set; }
    public string LogType { get; set; }
    public string? DriverQatarIdUrl { get; set; }

    public FuelLog FuelLog { get; set; }

    [NotMapped]
    public string Track { get; set; }
    public bool IsLessThan(int currentReading) => Reading < currentReading;
    public bool IsLessThanPrevious(int currentReading) => PreviousReading < currentReading;

}