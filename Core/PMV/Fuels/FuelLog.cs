using BaseEntityPack.Core;
using Core.Common.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.PMV.Fuels;

public class FuelLog : AggregateRoot<Guid>
{

    public static FuelLog Clone(FuelLog log)
    {
        var other = (FuelLog)log.MemberwiseClone();
        if(log.FuelTransactions.Count > 0)
        {
            other.FuelTransactions = new List<FuelTransaction>(log.FuelTransactions);
        }
        return other;
    }

  

    public static FuelLog Create(
        string stationCode,
        int lastDocumentNo,
        int referenceNo,
        DateTime? shiftStartTime,
        DateTime? shiftEndTime,
        int startShiftTankerKm,
        int endShiftTankerKm,
        float openingMeter,
        float openingBalance,
        float adjustmentBalance,
        float adjustmentMeter)
    {
        return new FuelLog(
            Guid.NewGuid(),
            stationCode,
            lastDocumentNo,
            referenceNo,
            shiftStartTime,
            shiftEndTime,
            startShiftTankerKm,
            endShiftTankerKm,
            openingMeter + adjustmentMeter, 
            openingBalance + adjustmentBalance);
    }


    //full entry
    public FuelLog(Guid id,
        string stationCode,
        int lastDocumentNo,
        int referenceNo,
        DateTime? shiftStartTime,
        DateTime? shiftEndTime,
        int startShiftTankerKm,
        int endShiftTankerKm,
        float openingMeter,
        float openingBalance) : base(id) {

        Id = id;
        StationCode = stationCode;
        ReferenceNo = referenceNo;
        DocumentNo = lastDocumentNo + 1;
        ShiftStartTime = shiftStartTime;
        ShiftEndTime = shiftEndTime;
        StartShiftTankerKm = startShiftTankerKm;
        EndShiftTankerKm= endShiftTankerKm;
        OpeningMeter = openingMeter;
        OpeningBalance= openingBalance;
        

        FuelTransactions = new List<FuelTransaction>();
        
        CreatedAt= DateTime.Now;
    }

    public string StationCode { get; set; }
    public DateTime Date { get; set; }
    public int ReferenceNo { get; set; }
    public int DocumentNo { get; set; }
    public float OpeningMeter { get; private set; }
    public float OpeningBalance { get; private set; }

    [NotMapped]
    public float ClosingMeter => (OpeningMeter) + TotalDispense;
    [NotMapped]
    public float TotalRestock => FuelTransactions
        .Where(t => t.LogType == EnumLogType.Restock.ToString())
        .Sum(c => c.GetActualQuantity());

    [NotMapped]
    public float TotalDispense => FuelTransactions
        .Where(t => t.LogType == EnumLogType.Dispense.ToString())
        .Sum(c => c.GetDispenseQuantity());

    [NotMapped]
    public float RemainingBalance => (OpeningBalance - TotalDispense) + TotalRestock;
    
    public DateTime? ShiftStartTime { get; set; }
    public DateTime? ShiftEndTime { get; set; }
    public int? StartShiftTankerKm { get; set; }
    public int? EndShiftTankerKm { get; set; }
    public string? Remarks { get; set; } = null;
    public string Fueler { get; set; } = "";
    public int LocationId { get; set; }
    public PostingObject Post { get; set; } = new PostingObject();
    public IList<FuelTransaction> FuelTransactions { get; set; }

    public void AddRestockTransaction(string assetCode, DateTime fuelDateTime, float qty)
    {
        var restock = new FuelTransaction(Guid.NewGuid(),
            assetCode,
            this.StationCode,
            fuelDateTime,
            qty);
        
        restock.Track = "add";
        
        FuelTransactions.Add(restock);
       
    }

    public void UpdateDetail(
        string detailId, 
        string refillStation, 
        string assetCode, 
        int reading, 
        float quantity, 
        string operatorDriver, 
        string transactionType)
    {

        var existingDetail = FuelTransactions.Where(d => d.Id == Guid.Parse(detailId)).FirstOrDefault();
        if (!existingDetail.IsLessThanPrevious(reading))
        {
            throw new Exception("Current Reading must not be less than the previous reading");
        }

        existingDetail.FuelStation = refillStation;
        existingDetail.AssetCode = assetCode;
        existingDetail.Reading = reading;
        existingDetail.Quantity = quantity;
        existingDetail.Driver = operatorDriver;
        existingDetail.LogType = transactionType;
        existingDetail.Track = "update";
    }

    public void AddDispenseTransaction(string assetCode,
        int previousReading,
        int reading,
        string driver,
        DateTime fuelDateTime,
        float qty)
    {   

        var dispense = new FuelTransaction(Guid.NewGuid(),
            assetCode,
            previousReading,
            reading,
            driver,
            this.StationCode,
            fuelDateTime,
            qty);

        FuelTransactions.Add(dispense);
       
    }


}

