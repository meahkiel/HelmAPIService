namespace Applications.UseCases.PMV.ServiceAlerts.DTO;

public class ServiceAlertResponse
{
    
    public string Id { get; init; }
    public string GroupName { get; init; }
    public string Description { get; init; }

    public IEnumerable<ServiceAlertDetailResponse> Details {get;init;}
}


public record ServiceAlertDetailResponse {
    public string Id { get; init; }
    public string ServiceCode { get; init; }
    public int KmAlert { get; init; }
    public int KmInterval { get; init; }
}