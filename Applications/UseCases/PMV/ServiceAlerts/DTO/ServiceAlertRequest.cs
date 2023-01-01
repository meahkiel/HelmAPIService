namespace Applications.UseCases.PMV.ServiceAlerts.DTO;

public record ServiceAlertRequest
{
    public string? Id { get; set; }
    public string GroupName { get; set; }
    public string Description { get; set; }

    public string Assigned { get; set; }

    public string Category { get; set; }

    public IEnumerable<ServiceAlertDetailRequest> Details {get;set;}
}

public record ServiceAlertDetailRequest {

    public string? Id { get; set; }
    public string ServiceCode { get; set; }
    public int KmAlert { get; set; }
    public int KmInterval { get; set; }
    public bool MarkIsDelete { get; set; }
}