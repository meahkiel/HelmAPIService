using BaseEntityPack.Core;
using Core.Common.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.PMV.Fuels;

[AuditableAttribute]
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

    public FuelLog() : base(Guid.NewGuid())
    {
        
    }

    public static FuelLog Create(
        string stationCode,
        int locationId,
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
            locationId,
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
        int locationId,
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
        LocationId = locationId;
        ReferenceNo = referenceNo;
        DocumentNo = lastDocumentNo;
        Date = shiftStartTime;
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
    public DateTime? Date { get; set; }
    public int ReferenceNo { get; set; }
    public int DocumentNo { get; set; }
    public float OpeningMeter { get; private set; }
    public float OpeningBalance { get; private set; }
    public DateTime? ShiftStartTime { get; set; }
    public DateTime? ShiftEndTime { get; set; }
    public int? StartShiftTankerKm { get; set; }
    public int? EndShiftTankerKm { get; set; }
    public string? Remarks { get; set; } = null;
    public string Fueler { get; set; } = "";
    public int LocationId { get; set; }
    public PostingObject Post { get; set; } = new PostingObject();
    public IList<FuelTransaction> FuelTransactions { get; set; } = new List<FuelTransaction>();

    [NotMapped]
    public float ClosingMeter => (OpeningMeter) + TotalDispense;
    [NotMapped]
    public float TotalRestock => FuelTransactions
        .Where(t => t.LogType == EnumLogType.Restock.ToString())
        .Sum(c => c.GetActualQuantity());

    [NotMapped]
    public float TotalDispense
    {
        get
        {
            
            var transactions = FuelTransactions.Where(t => t.LogType == EnumLogType.Dispense.ToString()).ToList();
            return transactions.Sum(l => l.GetDispenseQuantity());
        }
    }

    [NotMapped]
    public float RemainingBalance => (OpeningBalance - TotalDispense) + TotalRestock;
    public void UpsertRestockTransaction(
        string assetCode, 
        DateTime fuelDateTime, 
        float qty,
        string? transactionId = null)
    {
        Guid guid = string.IsNullOrEmpty(transactionId) ? Guid.NewGuid() : Guid.Parse(transactionId);
        var existing = FuelTransactions.Where(t => t.Id == Guid.Parse(transactionId)).FirstOrDefault();
        if(existing == null) {
            existing = new FuelTransaction(guid,
                assetCode,
                this.StationCode,
                fuelDateTime,
                qty);

            existing.Track = "add";
            
            FuelTransactions.Add(existing);
        }
        else {
            existing.AssetCode = assetCode;
            existing.Quantity = qty;
            existing.LogType = EnumLogType.Restock.ToString();
            existing.Track = "update";
        }
    }
    
    public void UpsertDispenseTransaction(
        string assetCode, 
        int previousReading,
        int reading, 
        string operatorDriver,
        DateTime fuelDateTime,
        float quantity, 
        string driverQatarIdUrl = "",
        string? detailId = null)
    {

        Guid guid = string.IsNullOrEmpty(detailId) ? Guid.NewGuid() : Guid.Parse(detailId);
        
        if(!string.IsNullOrEmpty(detailId)) {
            var transaction = FuelTransactions.Where(d => d.Id == guid).FirstOrDefault();
            if(transaction == null) {
                transaction = new FuelTransaction(guid,assetCode,previousReading,
                                        reading, operatorDriver, this.StationCode,
                                        fuelDateTime,quantity);
                transaction.DriverQatarIdUrl = driverQatarIdUrl;
                transaction.Track = "add";
                FuelTransactions.Add(transaction);
            }
            else {

                if (!transaction.IsLessThanPrevious(reading)) {
                    throw new Exception("Current Reading must not be less than the previous reading");
                }
                
                transaction.AssetCode = assetCode;
                transaction.Reading = reading;
                transaction.Quantity = quantity;
                transaction.FuelDateTime = fuelDateTime;
                transaction.Driver = operatorDriver;
                transaction.LogType = EnumLogType.Dispense.ToString();
                transaction.Track = "update";
            }
        }
    }

    

    public void Edit(int startShiftTankerKm,int endShiftTankerKm,int locationId) {
        this.StartShiftTankerKm = startShiftTankerKm;
        this.EndShiftTankerKm = endShiftTankerKm;
        this.LocationId = locationId;
    }

    public void TogglePost() {
        if(this.Post.PostedAt != null && this.Post.IsPosted) { 
            Post = PostingObject.UnPost();
        }
        else {
            Post = PostingObject.Post();
        }
    } 
}

