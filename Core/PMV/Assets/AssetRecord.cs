namespace Core.PMV.Assets;

public class AssetRecord : BaseEntity<Guid>
{
    
    public int AssetId { get; set; }
    public int CurrentReading { get; set; }
    public DateTime? LastServiceDate { get; set; }
    public string? LatestTransactionId { get; set; }

    public string Source { get; set; }
    
    public DateTime? LastInspectionDate { get; set; }

}