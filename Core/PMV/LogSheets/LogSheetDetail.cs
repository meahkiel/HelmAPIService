

using System.ComponentModel.DataAnnotations.Schema;

namespace Core.PMV.LogSheets;




public enum TransactionTypeEnum {
    Restock,
    Dispense
}

[AuditableAttribute]
public class LogSheetDetail : BaseEntity<Guid>
{
    public string? RefillStation { get; set; } = "";
    public string AssetCode { get; set; } = "";
    public DateTime FuelTime { get; set; } = DateTime.Now;
    public string? OperatorDriver { get; set; } = "";
    public int Reading { get; set; } = 0;
    //readonly
    public int PreviousReading { get; private set; }
    public float Quantity { get; set; } = 0f;
    public string DriverQatarIdUrl { get; set; }
    public string TransactionType { get; set; }
    public LogSheet LogSheet { get; set; }

    [NotMapped]
    public string Track { get; set; }

    
    public bool IsLessThan(int currentReading) => Reading < currentReading;

    
    public bool IsLessThanPrevious(int currentReading) => PreviousReading < currentReading;

    
    public static LogSheetDetail Create(
        string? refillStation,
        string assetCode,
        string fuelTime,
        int reading,
        int previousReading,
        float quantity,
        string driverQatarIdUrl,
        string? operatorDriver,
        string transactionType)
    {

        return new LogSheetDetail
        {
            Id = Guid.NewGuid(),
            RefillStation = refillStation,
            AssetCode = assetCode,
            FuelTime = DateTime.Parse(fuelTime),
            Reading = reading,
            PreviousReading = previousReading,
            Quantity = quantity,
            DriverQatarIdUrl = driverQatarIdUrl,
            OperatorDriver = operatorDriver,
            TransactionType = transactionType,
            Track = "new"
        };
    }
}