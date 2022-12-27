namespace Core.PMV.Alerts;

public class AlertDetail : BaseEntity<Guid>
{
    public string ServiceCode { get; set; }
    public int KmAlert { get; set; }
    public int KmInterval { get; set; }

    public ServiceAlert ServiceAlert { get; set; }

    

}