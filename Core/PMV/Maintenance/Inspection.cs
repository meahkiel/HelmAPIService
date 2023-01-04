namespace Core.PMV.Maintenance;

public class Inspection : BaseEntity<Guid>
{
    
    public int referenceNo { get; set; }
    
    public string assetCode { get; set; }
    public string description { get; set; }
    public DateTime? date { get; set; } = null;
    public DateTime lastServiceDate { get; set; }
    public string category { get; set; }
    
    public string location { get; set; }

    public int sMU { get; set; }
    public int lastReading { get; set; }

    public string remarks { get; set; }

    public string inspectedBy { get; set; }

     public IEnumerable<CheckList> checkList { get; set; } = new List<CheckList>();
}

public class CheckList {
    public string id { get; set; }
    public string serviceCode { get; set; }
    public string description { get; set; }
    public string condition { get; set; }
    public string remarks { get; set; }
}