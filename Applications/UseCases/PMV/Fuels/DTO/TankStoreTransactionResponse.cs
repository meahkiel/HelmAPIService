namespace Applications.UseCases.PMV.Fuels.DTO;

public class TankStoreSummary
{
    public string StationCode { get; set; }

    public int DocumentNo { get; set; }
    
    public float OpeningMeter { get; set; }
    public float ClosingMeter { get; set; }
    public float TotalDispenseQty { get; set; }
    public float TotalRestock { get; set; }
}

public class TankStoreTransaction {
    public string AssetCode { get; set; }
    public DateTime? StartShiftDate { get; set; }
    public DateTime? EndShiftDate { get; set; }
    public float OpeningMeter { get; set; }
    public float ClosingMeter { get; set; }
    public float Quantity { get; set; }
    public string Fueler { get; set; }
}

public class TankStoreReport {
    
    public IEnumerable<TankStoreSummary> Summaries { get; set; } = new List<TankStoreSummary>();



}