namespace Applications.UseCases.PMV.ServiceAlerts.DTO;

public record ServiceAlertRequest
{
    public string Id { get; init; }
    public string GroupName { get; init; }
    public string Description { get; init; }

    public IEnumerable<ServiceAlertDetailRequest> Details {get;init;}
}

public record ServiceAlertDetailRequest {

    public string Id { get; init; }
    public string ServiceCode { get; init; }
    public int KmAlert { get; init; }
    public int KmInterval { get; init; }
    public bool MarkIsDelete { get; set; }
}