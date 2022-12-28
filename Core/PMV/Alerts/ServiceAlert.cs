
namespace Core.PMV.Alerts;


[Auditable]
public class ServiceAlert : BaseEntity<Guid>
{
    public ServiceAlert()
    {
        
    }
    
    public ServiceAlert(string groupName,string description,string assigned, string category)
    {
        Id = Guid.NewGuid();
        GroupName = groupName;
        Description = description;
        Assigned = assigned;
        Category = category;
    }


    public string GroupName { get; set; }
    public string Description { get; set; }
    public string Assigned { get; set; }
    public string Category { get; set; }
    public bool InActive { get; private set; } = false;
    
    private List<ServiceAlertDetail> _details = new List<ServiceAlertDetail>();

    public IEnumerable<ServiceAlertDetail> Details => new List<ServiceAlertDetail>(_details);

    public void AddDetail(string serviceCode,int kmAlert,int kmInterval,string employeeCode) {
        _details.Add(new ServiceAlertDetail 
            { 
                Id = Guid.NewGuid(),
                ServiceCode = serviceCode,
                KmAlert = kmAlert,
                KmInterval = kmInterval,
                CreatedAt = DateTime.Now,
                CreatedBy = employeeCode,
                Tracker = "a",
                ServiceAlert = this
            });
    }

    public void RemoveDetail(string Id) {

        var detail = _details.Where(d => d.Id == Guid.Parse(Id)).FirstOrDefault();
        if(detail != null) 
            _details.Remove(detail);
    }

    public void UpdateDetail(string id, string serviceCode,int kmAlert,int kmInterval,string employeeCode) {

        var detail = _details.Where(d => d.Id == Guid.Parse(id)).FirstOrDefault();
        if(detail != null) {
            detail.ServiceCode = serviceCode;
            detail.KmAlert = kmAlert;
            detail.KmInterval = kmInterval;
            detail.UpdatedAt = DateTime.Now;
            detail.UpdatedBy = employeeCode;
            detail.Tracker = "m";
        }
        
    }


}