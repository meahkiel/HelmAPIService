
namespace Core.PMV.Alerts;

public class ServiceAlert : BaseEntity<Guid>
{
    public ServiceAlert()
    {
        
    }
    
    public ServiceAlert(string groupName,string description)
    {
        Id = Guid.NewGuid();
        GroupName = groupName;
        Description = description;
    }


    public string GroupName { get; set; }
    public string Description { get; set; }
    
    private List<AlertDetail> _details = new List<AlertDetail>();

    public IEnumerable<AlertDetail> Details => new List<AlertDetail>(_details);

    public void AddDetail(string serviceCode,int kmAlert,int kmInterval,string employeeCode) {
        _details.Add(new AlertDetail 
            { 
                Id = Guid.NewGuid(),
                ServiceCode = serviceCode,
                KmAlert = kmAlert,
                KmInterval = kmInterval,
                CreatedAt = DateTime.Now,
                CreatedBy = employeeCode
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
        }
        
    }


}