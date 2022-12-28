namespace Applications.UseCases.PMV.ServiceAlerts.DTO;

public class ServiceAlertResponse
{
    public string Id { get; set; }
    public string GroupName { get; set; }
    public string Description { get; set; }
    public string Assigned { get; set; }
    public string Category { get; set; }
    public IEnumerable<ServiceAlertDetailResponse> Details {get;set;}
}


public class ServiceAlertDetailResponse {
    public string Id { get; set; }
    public string ServiceCode { get; set; }
    public int KmAlert { get; set; }
    public int KmInterval { get; set; }
}