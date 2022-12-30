namespace Applications.UseCases.PMV.Assets.DTO;


public class AssetListResponse {
     public int? Id { get; set; }
    public string AssetCode { get; set; } = "";
    public string AssetDesc { get; set; } = "";
    public string? CompanyCode { get; set; }
    
    public string Category { get; set; } = "";
    public string SubCategory { get; set; } = "";

    public string Model { get; set; }
    public int Year { get; set; }
}
public class AssetViewResponse
{
    public string Id { get; set; }
    public string? AssetCode { get; set; }
    public string? CompanyCode { get; set; }
    public string? Color { get; set; }
    public string? ChasisNo { get; set; }
    public string? VendorCode { get; set; }
    public string? LpoNo { get; set; }
    public string? RateType { get; set; }
    public string? AccountCategory { get; set; }
    public string? AssetDesc { get; set; }
    public string? ConditionRank { get; set; }

    public DateTime? FirsRegDate { get; set; }
    public string? EngineNo { get; set; }
    public float NetValue { get; set; }
    public string? RentOwned { get; set; }

    public bool? ModifiedAsset { get; set; }
    public int Rate { get; set; }
    public string? YearMakeModel { get; set; }

    public string Category { get; set; }
    public string SubCategory { get; set; }
    
    public int CurrentReading { get; set; }
    public DateTime? LastServiceDate { get; set; }
    public string? LatestTransactionId { get; set; }
    public DateTime? LastInspectionDate { get; set; }

    public IEnumerable<ServiceLogResponse> ServiceLogs { get; set; }
}

public class ServiceLogResponse {
    public int AssetId { get; set; }
    public string ServiceCode { get; set; } = null!;
    public string? TransactionId { get; set; }
    public string? Source { get; set; }
    public DateTime? LastServiceDate { get; set; }
    
    public int LastReading { get; set; }

    public int KmAlert { get; set; }
    public int KmInterval { get; set; }


    public virtual int AlertAtKm { get;set;}

    public virtual int IntervalAtKm {get;set;}
}
