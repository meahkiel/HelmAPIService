namespace Core.PMV.Fuels;

public enum EnumType {
    In,
    Out
}



public class FuelHeader : BaseEntity<Guid> {

    public int ReferenceNo { get; set; }
    public DateTime ShiftStartTime { get; set; }
    public DateTime? ShiftEndTime { get; set; }
    public int StartShiftTankerKm { get; set; }
    public int? EndShiftTankerKm { get; set; }
    public int StartShiftMeterReading { get; set; }
    public int? EndShiftMeterReading { get; set; }
    public string? Remarks { get; set; } = null;
    public string Fueler { get; set; } = "";
    public int LocationId { get; set; }
    public FuelStation StationCode { get; set; }

    public IList<FuelTransaction> Transactions { get; set; } = new List<FuelTransaction>();

    public void AddInTransaction(
        string id,
        float qty,
        string referenceNo = "",
        string remarks = "") {

        var transaction = Transactions.SingleOrDefault(t => t.Id == Guid.Parse(id));
        
        if(transaction == null) {
            Transactions.Add(new FuelTransaction {
                Id = Guid.NewGuid(),
                ReferenceNo = referenceNo,
                Remarks = remarks,
                Quantity = qty,
                RefillFrom = this.StationCode,
                Type = EnumType.In
            });
        }
        else {
            transaction.ReferenceNo = referenceNo;
            transaction.Remarks = remarks;
            transaction.Quantity = qty;
        }
    }

     public void AddOutTransaction(
        string id,
        string dispenseTo, 
        float qty,
        string referenceNo = "",
        string remarks = "") {

        var transaction = Transactions.SingleOrDefault(t => t.Id == Guid.Parse(id));
        
        if(transaction == null) {
            Transactions.Add(new FuelTransaction {
                Id = Guid.NewGuid(),
                DispenseTo = dispenseTo,
                ReferenceNo = referenceNo,
                Remarks = remarks,
                Quantity = qty,
                Type = EnumType.Out
            });
        }
        else {
            transaction.ReferenceNo = referenceNo;
            transaction.Remarks = remarks;
            transaction.Quantity = qty;
        }
    }
}

public class FuelTransaction : BaseEntity<Guid>
{

    public float Quantity { get; set; }
    public FuelStation? RefillFrom { get; set; }
    public string DispenseTo { get; set; }
    public string ReferenceNo { get; set; }
    public string Remarks { get; set; }
    public EnumType Type { get; set; } = EnumType.Out;

}